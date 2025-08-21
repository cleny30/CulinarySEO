using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderDAO _orderDAO;
        private readonly IStockDAO  _stockDAO;
        private readonly ICartDAO _cartDAO;
        private readonly IProductDAO _productDAO;
        private readonly IVoucherDAO _voucherDAO;
        private readonly IWarehouseDAO _warehouseDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderDAO orderDAO, IStockDAO stockDAO, ICartDAO cartDAO, IProductDAO productDAO, IVoucherDAO voucherDAO, IWarehouseDAO warehouseDAO, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderDAO = orderDAO;
            _stockDAO = stockDAO;
            _cartDAO = cartDAO;
            _productDAO = productDAO;
            _voucherDAO = voucherDAO;
            _warehouseDAO = warehouseDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto request)
        {
            try
            {
                var cart = await _cartDAO.GetCartDataAsync(request.CustomerId);
                if (cart == null || !cart.CartItems.Any())
                    throw new ValidationException("Cart is empty");

                // ✅ Xác định warehouse theo thành phố trong ShippingAddress
                // Ở đây giả sử bạn có method GetWarehouseByCityAsync(string city)
                var warehouse = await _warehouseDAO.GetWarehouseByCityAsync(request.ShippingCity);
                if (warehouse == null)
                    throw new ValidationException($"No warehouse found for city {request.ShippingCity}");

                decimal subtotal = 0m;
                var orderDetails = new List<OrderDetail>();
                var stocksToUpdate = new List<Stock>();
                var allocations = new List<ItemAllocationDto>();

                foreach (var item in cart.CartItems)
                {
                    var product = item.Product ?? await _productDAO.GetProductAsync(item.ProductId)
                        ?? throw new NotFoundException($"Product not found: {item.ProductId}");

                    // ✅ Lấy stock của product trong warehouse đã chọn
                    var stock = await _stockDAO.GetStockAsync(item.ProductId, warehouse.WarehouseId);
                    if (stock == null || stock.Quantity < item.Quantity)
                        throw new ValidationException($"Not enough stock for product '{product.ProductName}' in {warehouse.WarehouseName}");

                    stock.Quantity -= item.Quantity;
                    stocksToUpdate.Add(stock);

                    var lineSubtotal = product.Price * item.Quantity;
                    subtotal += lineSubtotal;

                    orderDetails.Add(new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid(),
                        ProductId = product.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        Subtotal = lineSubtotal
                    });

                    allocations.Add(new ItemAllocationDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        OrderedQty = item.Quantity,
                        Picks = new List<PerWarehousePickDto>
                {
                    new PerWarehousePickDto
                    {
                        WarehouseId = warehouse.WarehouseId,
                        WarehouseName = warehouse.WarehouseName,
                        PickedQty = item.Quantity,
                        DistanceKm = 0 
                    }
                }
                    });
                }

                // ========== Áp nhiều voucher ==========
                decimal totalDiscount = 0m;
                var orderVouchers = new List<OrderVoucher>();

                if (request.VoucherCodes != null && request.VoucherCodes.Any())
                {
                    var vouchers = await _voucherDAO.GetByCodesAsync(request.VoucherCodes);

                    foreach (var voucher in vouchers)
                    {
                        if (voucher.Status != VoucherStatus.Active)
                            throw new ValidationException($"Voucher {voucher.VoucherCode} is not active.");
                        if (voucher.StartDate > DateTime.UtcNow || voucher.EndDate < DateTime.UtcNow)
                            throw new ValidationException($"Voucher {voucher.VoucherCode} expired.");
                        if (voucher.UsageLimit > 0 && voucher.UsedCount >= voucher.UsageLimit)
                            throw new ValidationException($"Voucher {voucher.VoucherCode} reached usage limit.");
                        if (subtotal < voucher.MinOrderValue)
                            throw new ValidationException($"Voucher {voucher.VoucherCode} requires min order {voucher.MinOrderValue}.");

                        decimal discount = 0;
                        if (voucher.DiscountType == VoucherDiscountType.Percentage)
                        {
                            discount = subtotal * (voucher.DiscountValue / 100m);
                            if (voucher.MaxDiscountValue.HasValue && discount > voucher.MaxDiscountValue.Value)
                                discount = voucher.MaxDiscountValue.Value;
                        }
                        else if (voucher.DiscountType == VoucherDiscountType.Fixed)
                        {
                            discount = voucher.DiscountValue;
                        }

                        if (discount > subtotal - totalDiscount)
                            discount = subtotal - totalDiscount;

                        if (discount > 0)
                        {
                            orderVouchers.Add(new OrderVoucher
                            {
                                OrderVoucherId = Guid.NewGuid(),
                                VoucherId = voucher.VoucherId,
                                AppliedDiscount = discount,
                                AppliedAt = DateTime.UtcNow
                            });

                            voucher.UsedCount = (voucher.UsedCount ?? 0) + 1;
                            totalDiscount += discount;
                        }
                    }
                }

                var shippingFee = 20000m;
                var total = subtotal - totalDiscount + shippingFee;
                if (total < 0) total = 0;

                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    OrderStatus = OrderStatus.Pending,
                    ShippingAddress = request.ShippingAddress,
                    ShippingFee = shippingFee,
                    TotalPrice = total,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = false,
                    CreatedAt = DateTime.UtcNow
                };

                var initStatus = new OrderStatusHistory
                {
                    HistoryId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    Status = OrderStatus.Pending,
                    ChangedAt = DateTime.UtcNow,
                    ChangeNote = "Order created"
                };

                foreach (var d in orderDetails) d.OrderId = order.OrderId;
                foreach (var ov in orderVouchers) ov.OrderId = order.OrderId;

                var orderId = await _orderDAO.CreateOrderGraphAsync(order, orderDetails, initStatus, orderVouchers);

                await _cartDAO.DeleteCartAsync(cart.CartId);
                await _stockDAO.UpdateStocksAsync(stocksToUpdate);

                var resp = _mapper.Map<CheckoutResponseDto>(order);
                resp.Subtotal = subtotal;
                resp.Discount = totalDiscount;
                resp.Allocations = allocations;

                return resp;
            }
            catch (ValidationException) { throw; }
            catch (NotFoundException) { throw; }
            catch (DatabaseException) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Checkout unexpected error for customer {CustomerId}", request.CustomerId);
                throw new DatabaseException("Checkout failed");
            }
        }

    }
}
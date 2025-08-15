using BusinessObject.Models.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable



namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Seed data for Role
            migrationBuilder.InsertData(
                 table: "roles",
                 columns: new[] { "role_id", "description", "role_name" },
                 values: new object[,]
                 {
            { 1, "Quản lí", "Admin" },
            { 2, "Nhân viên", "Staff" },
            { 3, "Người vận chuyển", "Shipper" }
                 });

            //Seed data for manager
            migrationBuilder.InsertData(
        table: "managers",
        columns: new[]
        {
    "manager_id",  "password", "email", "full_name",
    "phone", "role_id", "created_at", "status"
        },
        values: new object[,]
        {
    {
        Guid.NewGuid(), // admin
        "AQAAAAIAAYagAAAAEK2axMrYmK+26cgOaYFkOHqYXJkDtPzpHoTqoVnE9iEzEY9Q507c9rGMJ71qg4awKw==",
        "admin@gmail.com",
        "Admin User",
        "0111222333",
        1,
        DateTime.UtcNow,
        UserStatus.Active.ToString()
    },
    {
        Guid.NewGuid(), // staff
        "AQAAAAIAAYagAAAAEGwzTJksBrsx/77arwOi0akV3h7cgmuwx+w8UCkiG0dWvMYZu5/UH6xTuLfFi1PtIA==",
        "staff@gmail.com",
        "Staff User",
        "0999888777",
        2,
        DateTime.UtcNow,
        UserStatus.Active.ToString()
    },
    {
        Guid.NewGuid(), // shipper
        "AQAAAAIAAYagAAAAEFU/k2/qR9NZem4aFktsHVr2KUmZhdyyoldnsYafTCftq5PM7QmnR4lGQO+fHpUhgw==",
        "shipper@gmail.com",
        "Shipper User",
        "0333222111",
        3,
        DateTime.UtcNow,
        UserStatus.Active.ToString()
    }
        }
    );

            //Seed data for customer
            migrationBuilder.InsertData(
                        table: "customers",
                        columns: new[]
                        {
        "customer_id", "password", "email", "full_name",
        "phone", "profile_pic", "status", "created_at"
                        },
                        values: new object[]
                        {
        Guid.NewGuid(),
        "AQAAAAIAAYagAAAAEDVpmlt9TqchZLDL9AV0woZ4TThB42D8rtsLA4aiO6MeeG5IQG+qmBxufO1pD8sigw==",
        "customer@gmail.com",
        "Customer User",
        "0555555555",
        "https://ibb.co/QqcmxWR",
        UserStatus.Active.ToString(), // UserStatus.Active
        DateTime.UtcNow
                        }
                    );

            //Seed data for Permission
            migrationBuilder.InsertData(
                        table: "permissions",
                        columns: new[] { "permission_id", "permission_name", "description" },
                        values: new object[,]
                        {
        { 1, "ViewDashboard", "Xem dashboard" },
        { 2, "ManageStaffAccount", "Quản lý tài khoản nhân viên (tạo, cập nhật, đổi trạng thái)" },
        { 3, "ManageOwnProfile", "Chỉnh sửa thông tin cá nhân" },
        { 4, "ManageProducts", "Quản lý tài sản phẩm (tạo, cập nhật, đổi trạng thái)" },
        { 5, "ManageProductCategories", "Xem dashboard" },
        { 6, "ManageStock", "Quản lý kho" },
        { 7, "ViewOrders", "Xem danh sách tất cả đơn hàng" },
        { 8, "UpdateOrderStatus", "Cập nhật trạng thái đơn hàng" },
        { 9, "AssignShipper", "Phân công shipper cho đơn hàng" },
        { 10, "ViewAllCustomers", "Xem danh sách khách hàng" },
        { 11, "ViewCustomerDetail", "Xem thông tin chi tiết khách hàng" },
        { 12, "UpdateCustomer", "Cập nhật trạng thái khách hàng" },
        { 13, "ManageVouchers", "Quản lí Vouchers (tạo, cập nhật, xoá)" },
        { 14, "ManageBlog", "Quản lý bài viết blog" },
        { 15, "ModerateComments", "Kiểm duyệt bình luận" },
        { 16, "ViewAssignedOrders", "Xem danh sách đơn hàng được giao" },
        { 17, "ViewAssignedOrderDetail", "Xem chi tiết đơn hàng cần giao" },
        { 18, "UpdateDeliveryStatus", "Cập nhật trạng thái đơn hàng khi giao" },
        { 19, "ViewDeliveryHistory", "Xem lịch sử giao hàng" }
                        }
                    );

            //Seed data for Role-Permission
            migrationBuilder.InsertData(
            table: "role_permissions",
            columns: new[] { "role_id", "permission_id" },
            values: new object[,]
            {
                // Admin
                { 1, 1 }, { 1, 2 }, { 1, 3 }, { 1, 4 }, { 1, 5 },
                { 1, 6 }, { 1, 7 }, { 1, 8 }, { 1, 9 }, { 1, 10 },
                { 1, 11 }, { 1, 12 }, { 1, 13 }, { 1, 14 }, { 1, 15 },
                { 1, 16 }, { 1, 17 }, { 1, 19 },

                // Staff
                { 2, 1 }, { 2, 3 }, { 2, 4 }, { 2, 5 }, { 2, 6 },
                { 2, 7 }, { 2, 8 }, { 2, 9 }, { 2, 10 }, { 2, 13 },
                { 2, 14 }, { 2, 15 }, { 2, 16 }, { 2, 17 }, { 2, 19 },

                // Shipper
                { 3, 3 }, { 3, 8 }, { 3, 16 }, { 3, 17 }, { 3, 18 }, { 3, 19 }
            }
        );

            //Seed data for Category
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "category_id", "category_image", "category_name", "created_at", "description" },
                values: new object[,]
                {
            { 1, "category_image", "Featured",DateTime.UtcNow, "Featured" },
            { 2, "category_image", "Thịt heo", DateTime.UtcNow, "Thịt heo" },
            { 3, "category_image", "Trái cây",DateTime.UtcNow, "Trái Cây" },
            { 4, "category_image", "Rau lá", DateTime.UtcNow, "Rau lá" },
            { 5, "category_image", "Củ quả", DateTime.UtcNow, "Củ quả" },
            { 6, "category_image", "Thịt gà, vịt, chim",DateTime.UtcNow, "Thịt gà, vịt, chim" }
                });

            //Seed data for product
            var rnd = new Random();

            // 1. Tạo list ProductId cố định
            var productIds = new List<Guid>();
            for (int i = 0; i < 50; i++)
            {
                productIds.Add(Guid.Parse($"00000000-0000-0000-0000-{(i + 1).ToString("D12")}"));
            }

            // 2. Danh sách tên sản phẩm
            var productNames = new Dictionary<int, string[]>
{
    { 2, new [] { "Ba rọi", "Sườn non", "Thịt nạc", "Giò heo", "Thịt xay", "Sườn già", "Lòng heo", "Gan heo", "Thịt ba chỉ", "Xương ống" } },
    { 3, new [] { "Táo", "Cam", "Chuối", "Dưa hấu", "Nho", "Xoài", "Lê", "Chôm chôm", "Dâu tây", "Thanh long" } },
    { 4, new [] { "Rau muống", "Rau cải", "Cải bó xôi", "Rau ngót", "Rau thơm", "Xà lách", "Bắp cải", "Cải thìa", "Rau dền", "Rau tần ô" } },
    { 5, new [] { "Khoai tây", "Khoai lang", "Cà rốt", "Củ cải trắng", "Củ cải đỏ", "Sắn", "Củ hành", "Tỏi", "Gừng", "Củ riềng" } },
    { 6, new [] { "Gà ta", "Gà công nghiệp", "Vịt xiêm", "Vịt cỏ", "Chim bồ câu", "Chim cút", "Gà ác", "Gà tre", "Gà nòi", "Gà đông tảo" } }
};

            // 3. Insert Products
            int productIndex = 0;
            foreach (var category in productNames)
            {
                foreach (var name in category.Value)
                {
                    var productId = productIds[productIndex];
                    var price = rnd.Next(20000, 200000);
                    var discount = (productIndex % 3 == 0) ? rnd.Next(5, 20) : (int?)null;

                    migrationBuilder.InsertData(
                        table: "products",
                        columns: new[] { "product_id", "product_name", "description", "price", "discount", "created_at" },
                        values: new object[]
                        {
                productId,
                name,
                $"Sản phẩm {name} tươi ngon.",
                price,
                discount,
                DateTime.UtcNow
                        }
                    );

                    migrationBuilder.InsertData(
                        table: "product_category_mappings",
                        columns: new[] { "product_id", "category_id" },
                        values: new object[]
                        {
                productId,
                category.Key
                        }
                        );

                    productIndex++;
                }
            }

            // 4. Insert ProductImages
            for (int i = 0; i < productIds.Count; i++)
            {
                migrationBuilder.InsertData(
                    table: "product_images",
                    columns: new[] { "image_id", "product_id", "image_url", "is_primary", "created_at" },
                    values: new object[]
                    {
            Guid.Parse($"10000000-0000-0000-0000-{(i * 2 + 1).ToString("D12")}"),
            productIds[i],
            $"product_{i + 1}_main.jpg",
            true,
            DateTime.UtcNow
                    }
                );

                migrationBuilder.InsertData(
                    table: "product_images",
                    columns: new[] { "image_id", "product_id", "image_url", "is_primary", "created_at" },
                    values: new object[]
                    {
            Guid.Parse($"10000000-0000-0000-0000-{(i * 2 + 2).ToString("D12")}"),
            productIds[i],
            $"product_{i + 1}_extra.jpg",
            false,
            DateTime.UtcNow
                    }
                );
            }

            migrationBuilder.InsertData(
                table: "warehouses",
                columns: new[] { "warehouse_id", "warehouse_name", "location", "created_at" },
                values: new object[,]
                {
                {"191351cd-2eea-4aad-9bbd-0e7d31354840", "Cần Thơ WareHouse", "Cần Thơ", DateTime.UtcNow},
                {"8e9efc06-0af5-4c78-b074-2cbccbdf48a3", "HCM WareHouse", "Hồ Chí Minh", DateTime.UtcNow},
                }
            );

            migrationBuilder.InsertData(
                table: "stocks",
                columns: new[] { "stock_id", "product_id", "warehouse_id", "quantity", "last_updated" },
                values: new object[,]
                {
                {"b8efd839-8b34-4e98-acc3-9e8cf265385a",productIds[0], "191351cd-2eea-4aad-9bbd-0e7d31354840", 50,  DateTime.UtcNow},
                {"236ca3d2-c4c1-45a6-af11-9d7de437f25f",productIds[0],"8e9efc06-0af5-4c78-b074-2cbccbdf48a3", 25, DateTime.UtcNow},
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa Role-Permissions
            migrationBuilder.Sql("DELETE FROM role_permissions");

            // Xóa Permissions
            migrationBuilder.Sql("DELETE FROM permissions");

            // Xóa Product Images
            migrationBuilder.Sql("DELETE FROM product_images");

            // Xóa Products
            migrationBuilder.Sql("DELETE FROM products");

            // Xóa Categories
            migrationBuilder.Sql("DELETE FROM categories");

            // Xóa Customers
            migrationBuilder.Sql("DELETE FROM customers");

            // Xóa Managers
            migrationBuilder.Sql("DELETE FROM managers");

            // Xóa Roles
            migrationBuilder.Sql("DELETE FROM roles");

            migrationBuilder.Sql("DELETE FROM product_category_mappings");

            migrationBuilder.Sql("DELETE FROM warehouses");

            migrationBuilder.Sql("DELETE FROM stocks");

        }
    }
}

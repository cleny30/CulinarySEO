import { useEffect, useMemo, useState, memo, lazy } from 'react'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../ui/select'
import { shallowEqual, useDispatch, useSelector } from 'react-redux'
import { fetchProducts } from '@/redux/productview/apiRequest'
import type { RootState } from '@/redux/store'
import {
    Pagination,
    PaginationContent,
    PaginationItem,
    PaginationLink,
    PaginationNext,
    PaginationPrevious,
} from "@/components/ui/pagination"
import { useDebounce } from '@/utils/hooks/useDebounce'
import { setSortBy } from '@/redux/product/productfilterSlice'
const ProductGridView = lazy(() => import('./ProductGridView'))
const MobileFilterSider = lazy(() => import('./MobileFilterSider'))
function ShopProduct() {
    const dispatch = useDispatch();
    const products = useSelector((state: RootState) => state.productview, shallowEqual);
    const filter = useSelector((state: RootState) => state.productfilter, shallowEqual);
    const [page, setPage] = useState(1);


    // Use a static max price for the entire catalog (not per page)
    const maxPrice = 500000;
    // Build API params from Redux filter state
    const apiParams = useMemo(() => {
        const selectedIds =
            filter.productfilter?.selectedCategories && filter.productfilter.selectedCategories.length > 0
                ? filter.productfilter.selectedCategories
                : null;

        // If price is null (initial), do not filter
        const priceFrom = filter.productfilter?.price?.from ?? null;
        const priceTo = filter.productfilter?.price?.to ?? null;

        // Consider price "inactive" if it still spans the full range [0, maxPrice]
        const minPrice =
            priceFrom !== null && priceFrom > 0 ? priceFrom : null;

        const maxPriceParam =
            priceTo !== null && priceTo <= maxPrice ? priceTo : null;

        const isAvailable =
            filter.productfilter?.availability ?? null;

        const sortBy =
            filter.productfilter?.sortBy ?? null;
        const warehouseId =
            filter.productfilter?.WarehouseId ?? null
        return {
            CategoryIds: selectedIds,
            MinPrice: minPrice,
            MaxPrice: maxPriceParam,
            IsAvailable: isAvailable,
            SortBy: sortBy,
            warehouseId: warehouseId,
        };
    }, [filter, maxPrice]);

    // Debounce the whole params object
    const debouncedParams = useDebounce(apiParams, 400);

    // Reset page to 1 when filters (not page) change
    useEffect(() => {
        setPage(1);
    }, [
        debouncedParams.CategoryIds,
        debouncedParams.MinPrice,
        debouncedParams.MaxPrice,
        debouncedParams.IsAvailable,
        debouncedParams.SortBy,
        debouncedParams.warehouseId,
    ]);

    const totalItems = products.products?.totalItems ?? 0;
    const pageSize = products.products?.pageSize ?? 1;
    const totalPages = Math.ceil(totalItems / pageSize);
    useEffect(() => {
        fetchProducts(
            dispatch,
            page,
            debouncedParams.CategoryIds,
            debouncedParams.MinPrice,
            debouncedParams.MaxPrice,
            debouncedParams.IsAvailable,
            debouncedParams.SortBy,
            debouncedParams.warehouseId,
        );
    }, [page, debouncedParams]);
    return (
        <section className='px-[15px] lg:w-3/4 w-full'>
            <div className='w-full'>
                <img src={'/img/promotion_banner.webp'} alt="promotion-banner" loading="eager" fetchPriority="high" width={1000}
                    height={300} />
            </div>
            <div className='w-full flex items-center justify-between border-b-1 py-[15px]'>
                <div className='lg:hidden w-full flex justify-start items-center'>
                    <MobileFilterSider />
                </div>
                <div className='w-full flex items-center gap-2 justify-end'>
                    <strong className='text-sm'>Sort by:</strong>
                    <Select
                        defaultValue={'0'}
                        onValueChange={(val) => dispatch(setSortBy(val))} >
                        <SelectTrigger className="w-min-[200px] w-fit">
                            <SelectValue />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="0">Featured</SelectItem>
                            <SelectItem value="5">Best selling</SelectItem>
                            <SelectItem value="3">Alphabetically, A-Z</SelectItem>
                            <SelectItem value="4">Alphabetically, Z-A</SelectItem>
                            <SelectItem value="1">Price, low to high</SelectItem>
                            <SelectItem value="2">Price, high to low</SelectItem>
                            <SelectItem value="6">Date, new to old</SelectItem>
                            <SelectItem value="7">Date, old to new</SelectItem>
                        </SelectContent>
                    </Select>
                </div>
            </div>
            <div className='w-full'>
                <ProductGridView products={products.products} />
            </div>
            {products && totalPages > 1 && (
                <div className="flex justify-center mt-6">
                    <Pagination>
                        <PaginationContent>
                            <PaginationItem>
                                <PaginationPrevious
                                    onClick={() => setPage((p) => Math.max(1, p - 1))}
                                    aria-disabled={page === 1}
                                />
                            </PaginationItem>

                            {Array.from({ length: totalPages }).map((_, i) => (
                                <PaginationItem key={i}>
                                    <PaginationLink
                                        onClick={() => setPage(i + 1)}
                                        isActive={page === i + 1}
                                    >
                                        {i + 1}
                                    </PaginationLink>
                                </PaginationItem>
                            ))}

                            <PaginationItem>
                                <PaginationNext
                                    onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
                                    aria-disabled={page === totalPages}
                                />
                            </PaginationItem>
                        </PaginationContent>
                    </Pagination>
                </div>
            )}
        </section>
    )
}

export default memo(ShopProduct)

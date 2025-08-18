
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../ui/select'
import { Toggle } from '../ui/toggle'
import { Grid2X2, List } from 'lucide-react'
import ProductGridView from './ProductGridView'
import { useEffect, useMemo, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
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
import { getMaxPrice } from '@/utils/constants/product/product'

export default function ShopProduct() {
    const dispatch = useDispatch();
    const products = useSelector((state: RootState) => state.productview);
    const filter = useSelector((state: RootState) => state.productfilter);
    const [page, setPage] = useState(1);

    console.log(filter.productfilter.price)
    // const getProduct = async () => (
    //     await fetchProducts(dispatch,
    //         page,
    //         filter.productfilter.selectedCategories ?? null,
    //         filter.productfilter?.price?.from ?? null,
    //         filter.productfilter?.price?.to ?? null,
    //         filter.productfilter.availability ?? null,
    //         filter.productfilter.sortBy ?? null
    //     )
    // )
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

        return {
            CategoryIds: selectedIds,
            MinPrice: minPrice,
            MaxPrice: maxPriceParam,
            IsAvailable: isAvailable,
            SortBy: sortBy,
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
            debouncedParams.SortBy
        );
    }, [dispatch, page, debouncedParams]);

    console.log("Products:", products.products);
    return (
        <section className='px-[15px] w-3/4'>
            <div className='w-full'>
                <img src="/promotion_banner.webp" alt="" />
            </div>
            <div className='w-full flex items-center justify-between border-b-1 py-[15px]'>
                <div className='flex items-center gap-2'>
                    <Toggle >
                        <Grid2X2 />
                    </Toggle>
                    <Toggle>
                        <List />
                    </Toggle>
                </div>
                <div className='flex items-center gap-2'>
                    <strong className='text-sm'>Sort by:</strong>
                    <Select defaultValue="featured" >
                        <SelectTrigger className="w-min-[200px] w-fit">
                            <SelectValue />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="featured">Featured</SelectItem>
                            <SelectItem value="best-selling">Best selling</SelectItem>
                            <SelectItem value="a-z">Alphabetically, A-Z</SelectItem>
                            <SelectItem value="z-a">Alphabetically, Z-A</SelectItem>
                            <SelectItem value="price-low-high">Price, low to high</SelectItem>
                            <SelectItem value="price-high-low">Price, high to low</SelectItem>
                            <SelectItem value="date-new-old">Date, new to old</SelectItem>
                            <SelectItem value="date-old-new">Date, old to new</SelectItem>
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

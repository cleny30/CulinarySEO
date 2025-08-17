
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../ui/select'
import { Toggle } from '../ui/toggle'
import { Grid2X2, List } from 'lucide-react'
import ProductGridView from './ProductGridView'
import { useEffect, useState } from 'react'
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

export default function ShopProduct() {
    const dispatch = useDispatch();
    const products = useSelector((state: RootState) => state.productview);
    const [page, setPage] = useState(1);

    const getProduct = async () => (
        await fetchProducts(dispatch, page)
    )

    const totalItems = products.products?.totalItems ?? 0;
    const pageSize = products.products?.pageSize ?? 1;
    const totalPages = Math.ceil(totalItems / pageSize);
    useEffect(() => {
        getProduct()
    }, [page])
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
                            <SelectItem value="best-sell">Best selling</SelectItem>
                            <SelectItem value="A-Z">Alphabetically, A-Z</SelectItem>
                            <SelectItem value="Z-A">Alphabetically, Z-A</SelectItem>
                            <SelectItem value="price-low">Price, low to high</SelectItem>
                            <SelectItem value="price-high">Price, high to low</SelectItem>
                            <SelectItem value="newest">Date, new to old</SelectItem>
                            <SelectItem value="oldest">Date, old to new</SelectItem>
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

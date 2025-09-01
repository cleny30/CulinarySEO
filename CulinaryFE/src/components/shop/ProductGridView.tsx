import type { RootState } from '@/redux/store';
import { shallowEqual, useSelector } from 'react-redux'
import type { ProductResult } from '@/types/product';
import { Skeleton } from '../ui/skeleton';
import { lazy } from 'react';
const ProductGridCard = lazy(() => import('./ProductGridCard'))

export default function ProductGridView({ products }: { products: ProductResult | null }) {
    const fetchingproducts = useSelector((state: RootState) => state.productview.fetching, shallowEqual);
    
    return (
        <div className="grid gap-6 grid-cols-4 mt-[15px] w-full">
            {
                fetchingproducts
                    ?
                    (
                        <>
                            <Skeleton className="h-[289px] w-[231px] rounded-xl" />
                            <Skeleton className="h-[289px] w-[231px] rounded-xl" />
                            <Skeleton className="h-[289px] w-[231px] rounded-xl" />
                            <Skeleton className="h-[289px] w-[231px] rounded-xl" />
                        </>
                    )
                    :
                    (
                        products?.items.map((product) => (
                            <ProductGridCard key={product.productId} product={product} />
                        ))
                    )
            }
        </div>
    )
}

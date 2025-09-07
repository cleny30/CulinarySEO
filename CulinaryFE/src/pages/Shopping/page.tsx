import { useSyncFilterWithUrl } from '@/utils/hooks/useSyncFilterWithUrl';
import { lazy } from 'react'
const FIlterContainer = lazy(() => import('@/components/shop/FIlterContainer'));
const ShopProduct = lazy(() => import('@/components/shop/ShopProduct'));
const ShopTitle = lazy(() => import('@/components/shop/ShopTitle'));

export default function ShoppingPage() {
    useSyncFilterWithUrl(); //handle sync filter state with url query params
    return (
        <article className='py-20'>
            <ShopTitle />
            <div className='flex px-[15px] max-w-[1400px] w-full mx-auto'>
                <FIlterContainer />
                <ShopProduct />
            </div>
        </article>
    )
}

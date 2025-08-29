
import FIlterContainer from '@/components/shop/FIlterContainer';
import FIlterSider from '@/components/shop/FIlterSider';
import ShopProduct from '@/components/shop/ShopProduct';
import ShopTitle from "@/components/shop/ShopTitle";

export default function ShoppingPage() {
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

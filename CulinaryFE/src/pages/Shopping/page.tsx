import FIlterSider from "@/components/shop/FIlterSider";
import ShopProduct from '@/components/shop/ShopProduct';

export default function ShoppingPage() {
    return (
        <article className='py-20'>
            <div className='flex px-[15px] max-w-[1400px] w-full mx-auto'>
                <FIlterSider />
                <ShopProduct />
            </div>
        </article>
    )
}

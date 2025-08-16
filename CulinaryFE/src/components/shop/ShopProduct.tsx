
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../ui/select'
import { Toggle } from '../ui/toggle'
import { Grid2X2, List } from 'lucide-react'
import ProductGridView from './ProductGridView'
import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchProducts } from '@/redux/productview/apiRequest'
import type { RootState } from '@/redux/store'

export default function ShopProduct() {
    const dispatch = useDispatch();
    const products = useSelector((state: RootState) => state.productview);
    const getProduct = async () => (
        await fetchProducts(dispatch)
    )

    useEffect(() => {
        getProduct()
    }, [])
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
                <ProductGridView products={products.products}/>
            </div>
        </section>
    )
}

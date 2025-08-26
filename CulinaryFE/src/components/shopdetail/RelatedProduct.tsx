import { fetchProducts } from '@/redux/productview/apiRequest'
import type { ProductDetail } from '@/types/productdetail'
import { useEffect } from 'react'
import { useDispatch } from 'react-redux'

export default function RelatedProduct({ productdetail }: { productdetail: ProductDetail | null }) {
    const dispatch = useDispatch()
    // useEffect(() => {
    //     fetchProducts(dispatch, productdetail?.categoryName)
    // }, [])
    return (
        <section className='pt-20'>
            <div className='max-w-[1400px] w-full h-full mx-auto px-[15px]'>
                <h2 className='text-2xl font-bold mb-4 text-center'>Related Products</h2>
                <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6'>
                    {Array.from({ length: 4 }).map((_, index) => (
                        <div key={index} className='border p-4 rounded-lg'>
                            <img src={`/img/product${index + 1}.jpg`} alt={`Product ${index + 1}`} className='w-full h-40 object-cover mb-4' />
                            <h3 className='text-lg font-semibold'>Product {index + 1}</h3>
                            <p className='text-gray-600 mt-2'>$19.99</p>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    )
}

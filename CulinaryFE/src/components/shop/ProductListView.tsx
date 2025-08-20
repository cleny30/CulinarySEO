import type { RootState } from '@/redux/store';
import type { ProductResult } from '@/types/product'
import { useSelector } from 'react-redux';
import { Badge } from '../ui/badge';
import { Star } from 'lucide-react';

export default function ProductListView({ products }: { products: ProductResult | null }) {
  const fetchingproducts = useSelector((state: RootState) => state.productview.fetching);
  return (
    <div className='flex flex-col gap-4 mt-[15px]'>
      {
        products?.items.map((product) => (
          <ul key={product.productId} className='flex items-center gap-4 p-4 hover:shadow-lg transition-shadow w-full'>
            <li className='w-[calc(100%-12px)] flex'>
              <div className='w-[calc(33.33%-20px)] relative'>
                <a href="" className='decoration-0 w-full h-full'>
                  <img src={"/foodholder.jpg"} alt="" className="w-full h-full object-contain" />
                </a>
                {product.discount && (
                  <Badge className="absolute top-2 right-2 bg-red-500 text-white px-2 py-1 text-xs font-bold">
                    {product.discount}%
                  </Badge>
                )}
              </div>
              <div className='w-[calc(66.67%+20px)] p-[30px] items-start justify-center flex flex-col gap-4'>
                <a href="" className='decoration-0'>{product.productName}</a>
                {product.discount && product.finalPrice ? (
                  <div className='text-sm flex items-center gap-2'>
                    <span className='text-mau-gia-san-pham font-bold'>
                      <span className='text-xs '>₫</span>
                      {product.finalPrice}
                    </span>
                    <span className='line-through text-gray-400'>
                      <span className='text-xs'>₫</span>
                      {product.price}
                    </span>
                  </div>
                ) :
                  (
                    <div className='text-sm'>
                      <span className='text-mau-gia-san-pham font-bold'>
                        <span className='text-xs'>₫</span>
                        {product.price}
                      </span>
                    </div>
                  )}
                <div className="flex items-center gap-1">
                  {Array.from({ length: 5 }).map((_, i) => (
                    <Star
                      key={i}
                      size={14}
                      className={i < (product.averageRating ?? 0) ? "fill-yellow-400 text-yellow-400" : "text-gray-300"}
                    />
                  ))}
                  <span className="text-xs text-gray-500">({product.reviewCount ?? 0})</span>
                </div>
              </div>
            </li>
          </ul>
        ))
      }
    </div>
  )
}

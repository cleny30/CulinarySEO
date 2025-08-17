import type { RootState } from '@/redux/store';
import { useSelector } from 'react-redux'
import { Card, CardContent } from '../ui/card';
import { Badge } from '../ui/badge';
import type { ProductResult } from '@/types/product';
import { Star } from 'lucide-react';


export default function ProductGridView({ products }: { products: ProductResult | null }) {
    const fetchingproducts = useSelector((state: RootState) => state.productview.fetching);
    console.log(products)
    return (
        <div className="grid gap-6 grid-cols-4 mt-[15px]">
            {products?.items.map((product) => (
                <Card key={product.productId} className="p-2 hover:shadow-lg transition-shadow">
                    <div className="relative">
                        {product.discount && (
                            <Badge className="absolute top-2 right-2 bg-red-500 text-white px-2 py-1 text-xs font-bold">
                                {product.discount}%
                            </Badge>
                        )}
                        <img
                            src={"/foodholder.jpg"}
                            alt={product.productName}
                            className="w-full h-48 object-cover rounded-lg"
                        />
                    </div>
                    <CardContent className='px-0'>
                        <h3 className="font-medium text-gray-800 text-sm">{product.productName}</h3>
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
                    </CardContent>

                </Card>
            ))}
        </div>
    )
}

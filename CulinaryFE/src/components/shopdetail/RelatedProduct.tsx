import { fetchProducts } from '@/redux/productview/apiRequest'
import type { RootState } from '@/redux/store'
import type { ProductDetail } from '@/types/productdetail'
import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Card, CardContent } from '../ui/card'
import { Star } from 'lucide-react'
import { formatCurrency } from '@/utils/constants/product/product'
import { Skeleton } from '../ui/skeleton'
import { useNavigate } from 'react-router-dom'
import { Badge } from '../ui/badge'
import { Carousel, CarouselContent, CarouselItem, CarouselNext, CarouselPrevious } from '../ui/carousel'

export default function RelatedProduct({ productdetail }: { productdetail: ProductDetail | null }) {
    const dispatch = useDispatch()
    const relatedProduct = useSelector((state: RootState) => (state.productview))
    const navigate = useNavigate()
    useEffect(() => {
        fetchProducts(dispatch, 1, productdetail?.categoryId)
    }, [])

    return (
        <section className='pt-20'>
            {
                relatedProduct.fetching
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
                        <div className="max-w-[1400px] w-full h-full mx-auto px-[15px]">
                            <h2 className="text-2xl font-bold mb-4 text-center">Related Products</h2>
                            <Carousel
                                opts={{
                                    align: "start",
                                }}
                                className="w-full"
                            >
                                <CarouselContent>
                                    {relatedProduct?.products?.items.slice(0, 8).map((product) => (
                                        <CarouselItem
                                            key={product.productId}
                                            className="basis-1/2 sm:basis-1/3 md:basis-1/4 lg:basis-1/5"
                                        >
                                            <Card
                                                className="p-2 hover:shadow-lg transition-shadow cursor-pointer"
                                                onClick={() => navigate(`/collection/${product.slug}/${product.productId}`)}
                                            >
                                                <div className="relative">
                                                    {product.discount && (
                                                        <Badge className="absolute top-2 right-2 bg-red-500 text-white px-2 py-1 text-xs font-bold">
                                                            {product.discount}%
                                                        </Badge>
                                                    )}
                                                    <img
                                                        src="/img/foodholder.jpg"
                                                        alt={product.productName}
                                                        className="w-full h-48 object-cover rounded-lg"
                                                    />
                                                </div>
                                                <CardContent className="px-0 pt-2">
                                                    <h3 className="font-medium text-gray-800 text-sm">{product.productName}</h3>
                                                    {product.discount && product.finalPrice ? (
                                                        <div className="text-sm flex items-center gap-2">
                                                            <span className="text-mau-gia-san-pham font-bold">
                                                                {formatCurrency(product.finalPrice)}
                                                            </span>
                                                            <span className="line-through text-gray-400">
                                                                {formatCurrency(product.price)}
                                                            </span>
                                                        </div>
                                                    ) : (
                                                        <div className="text-sm">
                                                            <span className="text-mau-gia-san-pham font-bold">
                                                                {formatCurrency(product.price)}
                                                            </span>
                                                        </div>
                                                    )}
                                                    <div className="flex items-center gap-1">
                                                        {Array.from({ length: 5 }).map((_, i) => (
                                                            <Star
                                                                key={i}
                                                                size={14}
                                                                className={
                                                                    i < (product.averageRating ?? 0)
                                                                        ? "fill-yellow-400 text-yellow-400"
                                                                        : "text-gray-300"
                                                                }
                                                            />
                                                        ))}
                                                        <span className="text-xs text-gray-500">({product.reviewCount ?? 0})</span>
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </CarouselItem>
                                    ))}
                                </CarouselContent>
                            </Carousel>
                        </div>
                    )
            }
        </section>
    )
}

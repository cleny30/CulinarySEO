import { Star } from "lucide-react";
import {
    Carousel,
    CarouselContent,
    CarouselItem,
    CarouselNext,
    CarouselPrevious,
} from "../ui/carousel";
import { useMemo, useState } from "react";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import type { ProductDetail } from "@/types/productdetail";
import { formatCurrency } from "@/utils/constants/product/product";



export default function ProductMain({ productdetail }: { productdetail: ProductDetail | null }) {
    const [quantity, setQuantity] = useState<number>(1)
    console.log(productdetail, 'product')
    const increase = () => setQuantity((prev) => prev + 1)
    const decrease = () => setQuantity((prev) => (prev > 1 ? prev - 1 : 1))
    const reviewCount= useMemo(()=>{
        return productdetail?.reviews.filter(obj => obj.rating).length
    },[productdetail])
    return (
        <section className="pt-20">
            <div className="max-w-[1400px] w-full h-full mx-auto px-[15px]">
                <div className="w-full h-full flex gap-8 items-start">
                    <div className="w-[calc(40%-30px)] flex flex-col gap-4">
                        <div className="w-full h-full">
                            <img src={"/img/foodholder.jpg"} alt="" className="object-fill" />
                        </div>
                        <div className="w-full">
                            <Carousel>
                                <CarouselContent>
                                    <CarouselItem className="md:basis-1/2 lg:basis-1/3">
                                        <img src={"/img/foodholder.jpg"} alt="" className="object-fill" />
                                    </CarouselItem>
                                    <CarouselItem className="md:basis-1/2 lg:basis-1/3">
                                        <img src={"/img/foodholder.jpg"} alt="" className="object-fill" />
                                    </CarouselItem>
                                    <CarouselItem className="md:basis-1/2 lg:basis-1/3">
                                        <img src={"/img/foodholder.jpg"} alt="" className="object-fill" />
                                    </CarouselItem>
                                    <CarouselItem className="md:basis-1/2 lg:basis-1/3">
                                        <img src={"/img/foodholder.jpg"} alt="" className="object-fill" />
                                    </CarouselItem>
                                </CarouselContent>
                            </Carousel>
                        </div>
                    </div>
                    <div className="flex flex-col gap-4 w-[calc(50%+30px)] h-full items-start justify-start">
                        <div className="pb-4 border-b-1">
                            <strong className="text-[40px]">{productdetail?.productName}</strong>
                        </div>
                        <div>
                            <span>Availability: </span>
                            <span></span>
                        </div>
                        <div className="flex items-center gap-1">
                            {Array.from({ length: 5 }).map((_, i) => (
                                <Star
                                    key={i}
                                    size={18}
                                    className={i < (productdetail?.averageRating ?? 0) ? "fill-yellow-400 text-yellow-400" : "text-gray-300"}
                                />
                            ))}
                            <span className="text-sm text-gray-500">({reviewCount ?? 0})</span>
                        </div>
                        <div>
                            {productdetail?.discount && productdetail?.finalPrice ? (
                                <div className='text-sm flex items-center gap-2'>
                                    <span className='text-mau-gia-san-pham font-bold'>
                                        {formatCurrency(productdetail.finalPrice)}
                                    </span>
                                    <span className='line-through text-gray-400'>
                                        {formatCurrency(productdetail.price)}
                                    </span>
                                </div>
                            ) :
                                (
                                    <div className='text-sm'>
                                        <span className='text-mau-gia-san-pham font-bold'>
                                            {productdetail?.price ? formatCurrency(productdetail.price) : ""}
                                        </span>
                                    </div>
                                )}
                        </div>
                        <div>
                            <span className="text-gray-500 text-sm">
                                {productdetail?.shortDescription}
                            </span>
                        </div>
                        <div className="flex items-center gap-4">
                            <span>Quantity: </span>
                            <div className="flex items-center border rounded-md">
                                <Button
                                    type="button"
                                    variant="ghost"
                                    size="sm"
                                    className="px-3"
                                    onClick={decrease}
                                >
                                    –
                                </Button>
                                <Input
                                    type="number"
                                    value={quantity}
                                    readOnly
                                    className="w-12 text-center border-none focus-visible:ring-0 focus-visible:ring-offset-0"
                                />
                                <Button
                                    type="button"
                                    variant="ghost"
                                    size="sm"
                                    className="px-3"
                                    onClick={increase}
                                >
                                    +
                                </Button>
                            </div>
                        </div>
                        <div className="flex items-center gap-4">
                            <Button
                                size={"xl"}>
                                Add to Cart
                            </Button>
                            <Button
                                size={"xl"}>
                                Buy it now
                            </Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}

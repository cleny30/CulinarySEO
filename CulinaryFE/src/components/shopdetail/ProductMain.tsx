import { Star } from "lucide-react";
import {
    Carousel,
    CarouselContent,
    CarouselItem,
    CarouselNext,
    CarouselPrevious,
} from "../ui/carousel";
import { useState } from "react";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import type { ProductDetail } from "@/types/productdetail";



export default function ProductMain({ productdetail }: {productdetail: ProductDetail | null}) {
    const [quantity, setQuantity] = useState<number>(1)

    const increase = () => setQuantity((prev) => prev + 1)
    const decrease = () => setQuantity((prev) => (prev > 1 ? prev - 1 : 1))
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
                            <strong className="text-[40px]">Product name</strong>
                        </div>
                        <div className="flex items-center gap-1">
                            {Array.from({ length: 5 }).map((_, i) => (
                                <Star
                                    key={i}
                                    size={18}
                                    className={i < (4 ?? 0) ? "fill-yellow-400 text-yellow-400" : "text-gray-300"}
                                />
                            ))}
                            <span className="text-sm text-gray-500">({4 ?? 0})</span>
                        </div>
                        <div>
                            <span>Availability: </span>
                            <span>12 in stock</span>
                        </div>
                        <div>
                            Product Price
                        </div>
                        <div>
                            <span className="text-gray-500 text-sm">Product description goes here. It should be detailed and informative.</span>
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

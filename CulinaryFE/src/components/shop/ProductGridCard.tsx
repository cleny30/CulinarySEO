import React from 'react'
import { memo } from "react";
import { Card, CardContent } from "../ui/card";
import { Badge } from "../ui/badge";
import { Star } from "lucide-react";
import { formatCurrency } from "@/utils/constants/product/product";
import { useNavigate } from "react-router-dom";
import type { ProductResult } from '@/types/product';
import { LazyLoadImage } from "react-lazy-load-image-component";

interface ProductCardProps {
    product: ProductResult["items"][0]; // single product
}


function ProductGridCard({ product }: ProductCardProps) {
    const navigate = useNavigate()
    return (
        <Card
            className="p-2 hover:shadow-lg transition-shadow"
            onClick={() => navigate(`/shop/${product.slug}/${product.productId}`)}
        >
            <div className="relative">
                {product.discount && (
                    <Badge className="absolute top-2 right-2 bg-red-500 text-white px-2 py-1 text-xs font-bold">
                        {product.discount}%
                    </Badge>
                )}
                <LazyLoadImage
                    src={"/img/foodholder.jpg"} 
                    alt={product.productName}
                    className="w-full h-48 object-cover rounded-lg"
                />
            </div>

            <CardContent className="px-0">
                <h3 className="font-medium text-gray-800 text-sm">
                    {product.productName}
                </h3>

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
                    <span className="text-xs text-gray-500">
                        ({product.reviewCount ?? 0})
                    </span>
                </div>
            </CardContent>
        </Card>
    );
}

export default memo(ProductGridCard);
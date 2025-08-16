import type { RootState } from '@/redux/store';
import { useSelector } from 'react-redux'
import { Card } from '../ui/card';
import { Badge } from '../ui/badge';
import type { ProductResult } from '@/types/product';


export default function ProductGridView({ products }: { products: ProductResult | null }) {
    const fetchingproducts = useSelector((state: RootState) => state.productview.fetching);

    return (
        <div className="grid gap-6 grid-cols-4">
            {products?.items.map((product) => (
                <Card key={product.productId} className="p-4 hover:shadow-lg transition-shadow">
                    <div className="relative mb-4">
                        {product.discount && (
                            <Badge className="absolute top-2 right-2 bg-red-500 text-white px-2 py-1 text-xs font-bold">
                                {product.discount}%
                            </Badge>
                        )}
                        <img
                            src={"/placeholder.svg"}
                            alt={product.productName}
                            className="w-full h-48 object-cover rounded-lg"
                        />
                    </div>
                    <h3 className="font-medium text-gray-800 text-sm">{product.productName}</h3>
                </Card>
            ))}
        </div>
    )
}

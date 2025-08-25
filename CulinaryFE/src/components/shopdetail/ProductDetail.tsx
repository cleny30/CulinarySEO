import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import type { ProductDetail } from "@/types/productdetail"

export default function ProductDetail({ productdetail }: { productdetail: ProductDetail | null }) {
    return (
        <section className='pt-20'>
            <div className='max-w-[1400px] w-full h-full mx-auto px-[15px]'>
                <Tabs defaultValue="description" className='w-full'>
                    <TabsList className='w-full justify-between'>
                        <TabsTrigger value='description' className="px-4 py-5 bg-mau-be data-[state=active]:border-b-3 data-[state=active]:border-mau-nau-vien data-[state=active]:bg-mau-be text-[16px] font-bold">Description</TabsTrigger>
                        <TabsTrigger value='reviews' className="px-4 py-5 bg-mau-be data-[state=active]:border-b-3 data-[state=active]:border-mau-nau-vien data-[state=active]:bg-mau-be text-[16px] font-bold">Reviews</TabsTrigger>
                    </TabsList>
                    <TabsContent value='description'>
                        <div
                            className="[&_h1]:text-2xl [&_h1]:font-bold [&_h1]:text-blue-600 
             [&_ul]:list-disc [&_ul]:pl-6 [&_ul]:text-gray-700 [&_ul]:text-base 
             [&_li]:mb-1 
             [&_.highlight]:bg-yellow-200 [&_.highlight]:font-semibold [&_.highlight]:px-2 [&_.highlight]:rounded"
                            dangerouslySetInnerHTML={{ __html: productdetail?.longDescription || "" }}
                        />
                    </TabsContent>
                    <TabsContent value='reviews'>
                        <div className='text-gray-600 text-sm'>
                            No reviews yet.
                        </div>
                    </TabsContent>
                </Tabs>
            </div>
        </section>
    )
}

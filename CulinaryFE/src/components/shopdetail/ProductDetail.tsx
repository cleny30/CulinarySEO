import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"

export default function ProductDetail() {
  return (
    <section className='pt-20'>
        <div className='max-w-[1400px] w-full h-full mx-auto px-[15px]'>
            <Tabs defaultValue="description" className='w-full'>
                <TabsList className='w-full justify-between'>
                    <TabsTrigger value='description' className="px-4 py-5 bg-mau-be data-[state=active]:border-b-3 data-[state=active]:border-mau-nau-vien data-[state=active]:bg-mau-be text-[16px] font-bold">Description</TabsTrigger>
                    <TabsTrigger value='reviews' className="px-4 py-5 bg-mau-be data-[state=active]:border-b-3 data-[state=active]:border-mau-nau-vien data-[state=active]:bg-mau-be text-[16px] font-bold">Reviews</TabsTrigger>
                </TabsList>
                <TabsContent value='description'>
                    <div className='text-gray-600 text-sm'>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                    </div>
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

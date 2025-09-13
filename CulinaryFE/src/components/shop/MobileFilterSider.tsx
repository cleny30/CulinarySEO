import { Sheet, SheetContent, SheetTrigger } from '../ui/sheet'
import { Button } from '../ui/button'
import { Filter } from 'lucide-react'
import FIlterSider from './FIlterSider'

export default function MobileFilterSider() {
    return (
        <div className="lg:hidden px-[15px]">
            <Sheet>
                <SheetTrigger asChild>
                    <Button variant="outline" className="w-full flex items-center gap-2">
                        <Filter className="h-4 w-4" />
                        Filters
                    </Button>
                </SheetTrigger>
                <SheetContent forceMount side="left" className="w-[80%] max-sm:w-full px-[15px] overflow-y-auto">
                    <FIlterSider />
                </SheetContent>
            </Sheet>
        </div>
    )
}

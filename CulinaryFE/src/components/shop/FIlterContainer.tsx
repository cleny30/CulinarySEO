import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet"
import { Button } from "@/components/ui/button"
import { Filter } from "lucide-react"
import FIlterSider from "./FIlterSider"

export default function FIlterContainer() {
  return (
    <>
      {/* Desktop Sidebar */}
      <aside className="hidden md:block w-3/12 px-[15px]">
        <FIlterSider />
      </aside>

      {/* Mobile Filter Trigger */}
      <div className="md:hidden px-[15px] mb-4">
        <Sheet>
          <SheetTrigger asChild>
            <Button variant="outline" className="w-full flex items-center gap-2">
              <Filter className="h-4 w-4" />
              Filters
            </Button>
          </SheetTrigger>
          <SheetContent forceMount side="left" className="w-[80%] sm:w-[400px] overflow-y-auto">
            <FIlterSider />
          </SheetContent>
        </Sheet>
      </div>
    </>
  )
}

import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet"
import { Button } from "@/components/ui/button"
import { Filter } from "lucide-react"
import FIlterSider from "./FIlterSider"

export default function FIlterContainer() {
  return (
    <>
      {/* Desktop Sidebar */}
      <aside className="hidden lg:block w-3/12 px-[15px]">
        <FIlterSider />
      </aside>
    </>
  )
}

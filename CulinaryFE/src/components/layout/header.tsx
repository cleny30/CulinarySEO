import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import HeaderLogo from "../header/header_logo";
import HeaderNav from "../header/header_nav";
import HeaderRightActions from "../header/header_right_actions";
import SubHeader from "./subheader";
import { cn } from "@/lib/utils";
import TopBar from "./topbar";

interface HeaderProps {
  headerStyle?: "1" | "2" | "3";
  subHeader?: boolean;
  topbar?: boolean;
}
export default function Header({
  subHeader = true,
  headerStyle = "1",
  topbar = true,
}: HeaderProps) {
  const user =
    useSelector((state: RootState) => state.auth.login?.currentUser) || null;
  const headerContainerStyle = "w-full flex justify-center";
  return (
    <>
      {headerStyle === "1" && (
        <header className="w-screen flex flex-col absolute top-0">
          {topbar && (
            <div className={cn(headerContainerStyle, "bg-mau-den p-2")}>
              <TopBar />
            </div>
          )}
          <div
            className={cn(
              headerContainerStyle,
              "border-b-2 border-b-mau-do-color"
            )}
          >
            <nav className="flex items-center w-full justify-between max-w-[1200px]">
              <HeaderLogo />
              <HeaderNav />
              <HeaderRightActions user={user} />
            </nav>
          </div>

          {subHeader && (
            <div className={cn(headerContainerStyle, "")}>
              <SubHeader />
            </div>
          )}
        </header>
      )}
    </>
  );
}

// <div className="block lg:hidden">
//   <div className="flex items-center justify-between">

//     <a href={logo.url} className="flex items-center gap-2">
//       <img
//         src={logo.src}
//         className="max-h-8 dark:invert"
//         alt={logo.alt}
//       />
//     </a>
//     <Sheet>
//       <SheetTrigger asChild>
//         <Button variant="outline" size="icon">
//           <Menu className="size-4" />
//         </Button>
//       </SheetTrigger>
//       <SheetContent className="overflow-y-auto">
//         <SheetHeader>
//           <SheetTitle>
//             <a href={logo.url} className="flex items-center gap-2">
//               <img
//                 src={logo.src}
//                 className="max-h-8 dark:invert"
//                 alt={logo.alt}
//               />
//             </a>
//           </SheetTitle>
//         </SheetHeader>
//         <div className="flex flex-col gap-6 p-4">
//           <Accordion
//             type="single"
//             collapsible
//             className="flex w-full flex-col gap-4"
//           >
//             {menu.map((item) => renderMobileMenuItem(item))}
//           </Accordion>

//           <div className="flex flex-col gap-3">
//             <Button asChild variant="outline">
//               <a href={auth.login.url}>{auth.login.title}</a>
//             </Button>
//             <Button asChild>
//               <a href={auth.signup.url}>{auth.signup.title}</a>
//             </Button>
//           </div>
//         </div>
//       </SheetContent>
//     </Sheet>
//   </div>
// </div>

//         const renderMobileMenuItem = (item: MenuItem) => {
//   if (item.items) {
//     return (
//       <AccordionItem key={item.title} value={item.title} className="border-b-0">
//         <AccordionTrigger className="text-md py-0 font-semibold hover:no-underline">
//           {item.title}
//         </AccordionTrigger>
//         <AccordionContent className="mt-2">
//           {item.items.map((subItem) => (
//             <SubMenuLink key={subItem.title} item={subItem} />
//           ))}
//         </AccordionContent>
//       </AccordionItem>
//     );
//   }

//   return (
//     <a key={item.title} href={item.url} className="text-md font-semibold">
//       {item.title}
//     </a>
//   );
// };

import React from "react";
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "../ui/sheet";
import { Button } from "../ui/button";
import { Icon } from "@/utils/assets/icon";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "../ui/accordion";
import { Link } from "react-router-dom";
import type { NavItem } from "@/types/home";
import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import type { UserSession } from "@/types/userSession";
import { Logo } from "@/assets/svg/logo";

export default function HeaderNavMobile({
  user,
}: {
  user?: UserSession | null;
}) {
  const header = useSelector((state: RootState) => state.home.header);
  return (
    <Sheet>
      <SheetTrigger asChild className="lg:hidden">
        <Button variant="ghost" size="icon" className="p-5 hover:bg-white/25">
          <Icon.Menu className="size-7" />
        </Button>
      </SheetTrigger>
      <SheetContent className="overflow-y-auto lg:hidden" side="top">
        <SheetHeader>
          <SheetTitle>
            {user ? (
              <Link
                to={`/account/profile`}
                className="flex items-center gap-x-2"
              >
                <Avatar className="size-12">
                  <AvatarImage
                    src={user.profilePic || "https://github.com/shadcn.png"}
                  />
                  <AvatarFallback>{user.fullName}</AvatarFallback>
                </Avatar>
                <span>{user.fullName}</span>
              </Link>
            ) : (
              <Link to={"/"} className={``}>
                <Logo color="#222222" />
              </Link>
            )}
          </SheetTitle>
        </SheetHeader>
        <div className="flex flex-col gap-6 p-4">
          <nav className="flex-1 lg:hidden">
            <Accordion type="single" collapsible className=" w-full">
              {header.categoryItem?.map((col: NavItem) => (
                <RenderMobileMenuItem key={col.label} item={col} />
              ))}
            </Accordion>
          </nav>

          <div className="flex flex-col gap-3"></div>
        </div>
      </SheetContent>
    </Sheet>
  );
}

const RenderMobileMenuItem = ({ item }: { item: NavItem }) => {
  if (item.children) {
    return (
      <AccordionItem value={item.label} className="border-none">
        <AccordionTrigger className="py-4 font-semibold uppercase text-base tracking-wider font-Lucky">
          {item.label}
        </AccordionTrigger>
        <AccordionContent>
          {item.isCategory ? (
            <ul className="flex flex-col gap-4 px-4">
              {item.children?.map((child) => (
                <li key={child.label}>
                  <Link
                    to={child.href || "/"}
                    className="font-medium text-base hover:text-red-500 flex items-center gap-x-2"
                  >
                    <Avatar className="size-12 rounded-md">
                      <AvatarImage
                        src={child.image || "https://github.com/shadcn.png"}
                        className="bg-gray-100"
                      />
                      <AvatarFallback>{child.label}</AvatarFallback>
                    </Avatar>
                    {child.label}
                  </Link>
                </li>
              ))}
            </ul>
          ) : (
            <ul className="flex flex-col gap-4 px-4">
              {item.children?.map((child,index) => (
                <li key={child.label} className="flex items-center gap-x-2">
                  <Link
                    to={child.href || "/"}
                    className="font-medium text-base hover:text-red-500 shrink-0"
                  >
                    {child.label}
                  </Link>
                  <hr className="min-w-[40px] w-full h-[1px] bg-mau-den/25 border-0 rounded-sm dark:bg-gray-700" />
                  <span className="font-Lucky text-mau-den/25 text-sm">
                    {index < 10 ? "0" + (index + 1) : index + 1}
                  </span>
                </li>
              ))}
            </ul>
          )}
        </AccordionContent>
      </AccordionItem>
    );
  }
  return (
    <Link
      to={item.href || "/"}
      className="block py-4 font-semibold uppercase text-base font-Lucky tracking-wider"
    >
      {item.label}
    </Link>
  );
};

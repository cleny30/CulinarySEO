import {
  NavigationMenu,
  NavigationMenuContent,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
  NavigationMenuTrigger,
} from "@/components/ui/navigation-menu";
import { cn } from "@/lib/utils";
import { Link } from "react-router-dom";
import { LazyLoadImage } from "react-lazy-load-image-component";
import type { NavItem } from "@/types/home";
import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import { Skeleton } from "../ui/skeleton";
import { motion, MotionValue } from "framer-motion";

const menuItemStyle =
  "text-mau-den font-Lucky text-lg bg-transparent justify-center";
const MotionMenuTrigger = motion(NavigationMenuTrigger);
const MotionMenuLink = motion(NavigationMenuLink);

export default function HeaderNav({
  motionHeight,
}: {
  motionHeight?: MotionValue<unknown>;
}) {
  const header = useSelector((state: RootState) => state.home.header);
  return (
    <NavigationMenu className="border-x-1 border-mau-do-color px-4 not-lg:hidden">
      <NavigationMenuList>
        {header.categoryItem?.map((item: NavItem) => {
          return (
            <NavigationMenuItem key={item.label}>
              {item.children ? (
                <>
                  <MotionMenuTrigger
                    className={cn("pr-3", menuItemStyle)}
                    style={{ height: motionHeight }}
                  >
                    {item.label}
                  </MotionMenuTrigger>
                  {item.isCategory ? (
                    <NavigationMenuContentCate
                      menuItems={item.children}
                      featureCats={item.featureCat!}
                      loading={header.loading}
                    />
                  ) : (
                    <NavigationMenuContentNormal menuItems={item.children} />
                  )}
                </>
              ) : (
                <MotionMenuLink
                  asChild
                  className={cn("px-5", menuItemStyle)}
                  style={{ height: motionHeight }}
                >
                  <Link to={item.href || "/"}>{item.label}</Link>
                </MotionMenuLink>
              )}
            </NavigationMenuItem>
          );
        })}
      </NavigationMenuList>
    </NavigationMenu>
  );
}

const NavigationMenuSkeleton = () => {
  return (
    <ul className="flex flex-col space-y-4 min-w-[200px] mr-6">
      <li className="flex items-center space-x-2">
        <Skeleton className="h-5 w-6 rounded-full" />
        <Skeleton className="h-5 w-full" />
      </li>
      <li className="flex items-center space-x-2">
        <Skeleton className="h-5 w-6 rounded-full" />
        <Skeleton className="h-5 w-full" />
      </li>
      <li className="flex items-center space-x-2">
        <Skeleton className="h-5 w-6 rounded-full" />
        <Skeleton className="h-5 w-full" />
      </li>
    </ul>
  );
};

const NavigationMenuContentCate = ({
  menuItems,
  featureCats,
  loading,
}: {
  menuItems: NavItem["children"];
  featureCats?: NavItem["featureCat"];
  loading?: boolean;
}) => {
  return (
    <NavigationMenuContent asChild className="p-4">
      <div className="flex flex-col lg:flex-row space-2">
        {loading ? (
          <NavigationMenuSkeleton />
        ) : (
          <ul className="grid grid-flow-col lg:grid-rows-4 md:grid-rows-6 gap-1 min-w-max">
            {menuItems?.map((item) => {
              return (
                <li key={item.label} className="w-50">
                  <NavigationMenuLink asChild>
                    <Link
                      to={item.href}
                      className="group flex flex-row items-center w-full gap-x-2 hover:bg-transparent"
                      style={{ padding: "0.25rem" }}
                    >
                      <LazyLoadImage
                        src={item.image}
                        alt={item.label + "_img"}
                        threshold={100}
                        className="aspect-square w-14 h-14 rounded-md bg-gray-100 group-hover:scale-105 duration-200"
                      />
                      <span className="text-sm transition-colors duration-200 group-hover:text-mau-do-color group-hover:font-semibold">
                        {item.label}
                      </span>
                    </Link>
                  </NavigationMenuLink>
                </li>
              );
            })}
          </ul>
        )}
        {featureCats && (
          <div className="flex lg:flex-col items-center gap-2">
            {featureCats.map((featureCat) => (
              <Link
                to={featureCat.url || "/"}
                key={featureCat.title}
                className="relative w-full lg:w-70 z-10 h-40 lg:h-full flex items-center justify-center rounded-xs overflow-hidden"
              >
                <img
                  src={featureCat.image}
                  alt={featureCat.title}
                  className="absolute inset-0 w-full h-full -z-1 object-cover brightness-80"
                />
                <h3 className="text-white font-semibold">{featureCat.title}</h3>
              </Link>
            ))}
          </div>
        )}
      </div>
    </NavigationMenuContent>
  );
};
const NavigationMenuContentNormal = ({
  menuItems,
  loading,
}: {
  menuItems: NavItem["children"];
  loading?: boolean;
}) => {
  return (
    <NavigationMenuContent asChild className="p-4">
      {loading ? (
        <NavigationMenuSkeleton />
      ) : (
        <ul className="pr-6">
          {menuItems?.map((item, index) => {
            return (
              <li key={item.label} className="flex items-center space-x-1.5">
                <NavigationMenuLink asChild className="text-base">
                  <Link to={item.href} className="text-nowrap font-Lucky">
                    {item.label}
                  </Link>
                </NavigationMenuLink>
                <hr className="min-w-[40px] w-full h-[1px] bg-mau-den/25 border-0 rounded-sm dark:bg-gray-700" />
                <span className="font-Lucky text-mau-den/25 text-sm">
                  {index < 10 ? "0" + (index + 1) : index + 1}
                </span>
              </li>
            );
          })}
        </ul>
      )}
    </NavigationMenuContent>
  );
};

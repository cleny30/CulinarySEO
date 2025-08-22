import {
  NavigationMenu,
  NavigationMenuContent,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
  NavigationMenuTrigger,
} from "@/components/ui/navigation-menu";
import { getCateList, menuNav, type NavItem } from "@/utils/config/navMenu";
import { cn } from "@/lib/utils";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { LazyLoadImage } from "react-lazy-load-image-component";

const menuItemStyle =
  "text-mau-den font-Lucky text-lg bg-transparent h-20 justify-center";

export default function HeaderNav() {
  const [dynamicMenuNav, setDynamicMenuNav] = useState<NavItem[]>(menuNav);

  useEffect(() => {
    const fetchCategories = async () => {
      const categories = await getCateList();
      if (categories) {
        const updatedMenuNav = menuNav.map((item) => {
          if (item.slug === "thuc-don") {
            return {
              ...item,
              children: categories,
            };
          }
          return item;
        });
        setDynamicMenuNav(updatedMenuNav);
      }
    };

    fetchCategories();
  }, []);
  return (
    <NavigationMenu className="border-x-2 border-mau-do-color">
      <NavigationMenuList>
        {dynamicMenuNav.map((item: NavItem) => {
          return (
            <NavigationMenuItem key={item.label}>
              {item.children ? (
                <>
                  <NavigationMenuTrigger className={cn("pr-3", menuItemStyle)}>
                    {item.label}
                  </NavigationMenuTrigger>
                  {item.isCategory ? (
                    <NavigationMenuContentCate
                      menuItems={item.children}
                      featureCats={item.featureCat!}
                    />
                  ) : (
                    <NavigationMenuContentNormal menuItems={item.children} />
                  )}
                </>
              ) : (
                <NavigationMenuLink
                  asChild
                  className={cn("px-5", menuItemStyle)}
                >
                  <Link to={item.href}>{item.label}</Link>
                </NavigationMenuLink>
              )}
            </NavigationMenuItem>
          );
        })}
      </NavigationMenuList>
    </NavigationMenu>
  );
}

const NavigationMenuContentCate = ({
  menuItems,
  featureCats,
}: {
  menuItems: NavItem["children"];
  featureCats?: NavItem["featureCat"];
}) => {
  return (
    <NavigationMenuContent asChild className="p-4">
      <div className="flex flex-col lg:flex-row space-y-2">
        <ul className="grid grid-flow-col lg:grid-rows-4 md:grid-rows-6 gap-x-1 min-w-max">
          {menuItems?.map((item) => {
            return (
              <li key={item.label} className="w-50">
                <NavigationMenuLink asChild>
                  <Link
                    to={item.href}
                    className="flex flex-row items-center w-full gap-x-2"
                  >
                    <LazyLoadImage
                      effect="blur"
                      src={item.image}
                      alt={item.label + "_img"}
                      className="aspect-square w-14 h-14 rounded-md"
                    />
                    <span className="text-sm">{item.label}</span>
                  </Link>
                </NavigationMenuLink>
              </li>
            );
          })}
        </ul>
        {featureCats && (
          <div className="flex lg:flex-col items-center gap-2">
            {featureCats.map((featureCat) => (
              <div
                key={featureCat.title}
                className="relative w-full lg:w-70 z-10 h-40 lg:h-full flex items-center justify-center rounded-xs overflow-hidden"
              >
                <LazyLoadImage
                  effect="blur"
                  src={featureCat.image}
                  alt={featureCat.title}
                  className="absolute inset-0 w-full h-full -z-1 object-cover brightness-80"
                />
                <h3 className="text-white font-semibold">{featureCat.title}</h3>
              </div>
            ))}
          </div>
        )}
      </div>
    </NavigationMenuContent>
  );
};
const NavigationMenuContentNormal = ({
  menuItems,
}: {
  menuItems: NavItem["children"];
}) => {
  return (
    <NavigationMenuContent asChild className="p-4">
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
    </NavigationMenuContent>
  );
};

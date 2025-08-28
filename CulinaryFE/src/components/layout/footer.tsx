import { Logo } from "@/assets/svg/logo";
import { storeInfo } from "@/storeInfo";
import { Icon } from "@/utils/assets/icon";
import { footerMenu } from "@/utils/config/navMenu";
import React from "react";
import { Link } from "react-router-dom";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";

const Footer: React.FC = () => {
  return (
    <footer className="bg-mau-sua-bo">
      <div className="flex flex-col">
        {/* =========================== Shop logo ================ */}
        <div className="border-b-1 border-mau-nau-vien justify-center flex not-md:hidden">
          <div className="container w-full flex justify-center relative p-14">
            <Link to={"/"}>
              <Logo color="#222222" className="h-24" />
            </Link>
          </div>
        </div>
        {/* =========================== Footer content ================ */}
        <div className="flex justify-center">
          <div className="container flex w-full px-1 gap-x-4 justify-between flex-wrap not-lg:flex-col">
            {/* ==================== Footer left heading ==================== */}
            <div className="flex items-start w-full md:w-[345px] gap-8">
              <Link to={"/"} className="mt-8 md:hidden">
                <Logo color="#222222" className="h-15 not-sm:h-12" />
              </Link>
              <div className="py-8 space-y-4">
                <h1 className="font-Lucky text-2xl uppercase">
                  {storeInfo.footer_heading}
                </h1>
                <p className="text-base">{storeInfo.footer_subheading}</p>
              </div>
            </div>
            {/* ================== Footer menu  =======================*/}
            <nav className="px-[1px] bg-gradient-to-b from-mau-nau-vien to-mau-sua-bo not-xl:flex-1 not-lg:hidden">
              <div className="flex items-start bg-mau-sua-bo px-8 justify-between gap-x-4">
                {footerMenu.map((col) => (
                  <div key={col.label} className="space-y-4 py-8 lg:p-4 xl:p-8">
                    <h1 className="font-Lucky text-2xl uppercase">
                      {col.label}
                    </h1>
                    <ul className="flex flex-col gap-y-2">
                      {col.children?.map((row) => (
                        <li key={row.label}>
                          <Link
                            to={row.href}
                            className="text-base duration-200 hover:text-mau-do-color"
                          >
                            {row.label}
                          </Link>
                        </li>
                      ))}
                    </ul>
                  </div>
                ))}
              </div>
            </nav>
            {/* ================== Footer menu  =======================*/}
            <nav className="flex-1 lg:hidden">
              <Accordion
                type="single"
                collapsible
                className="bg-mau-sua-bo w-full"
              >
                {footerMenu.map((col) => (
                  <AccordionItem
                    value={`item-${col.label}`}
                    key={col.label}
                    className=""
                  >
                    <AccordionTrigger className="font-Lucky text-2xl uppercase pr-4">
                      {col.label}
                    </AccordionTrigger>
                    <AccordionContent>
                      <ul className="flex flex-col gap-y-2 ml-4">
                        {col.children?.map((row) => (
                          <li key={row.label}>
                            <Link
                              to={row.href}
                              className="text-base duration-200 hover:text-mau-do-color"
                            >
                              {row.label}
                            </Link>
                          </li>
                        ))}
                      </ul>
                    </AccordionContent>
                  </AccordionItem>
                ))}
              </Accordion>
            </nav>
            {/* ======================= Footer Right Info ================== */}
            <div className="flex xl:flex-col p-8 gap-8  not-lg:flex-1 not-lg:px-0">
              <div className="space-y-4">
                <h1 className="font-Lucky uppercase text-2xl">Chi nhánh</h1>
                <ul className="space-y-2">
                  <li className="flex items-start text-sm gap-2 max-w-[200px]">
                    <Icon.MapPin className="w-5 h-5" />
                    <span>{storeInfo.store_address1}</span>
                  </li>
                  <li className="flex items-start text-sm gap-2 max-w-[200px]">
                    <Icon.MapPin className="w-5 h-5" />
                    <span>{storeInfo.store_address2}</span>
                  </li>
                </ul>
              </div>
              <div className="space-y-4">
                <h1 className="font-Lucky uppercase text-2xl">Liên hệ</h1>
                <ul className="space-y-2">
                  <li className="flex items-start text-sm gap-2 max-w-[200px]">
                    Sđt: {storeInfo.store_phone_number}
                  </li>
                  <li className="flex items-start text-sm gap-2 max-w-[200px]">
                    Email: {storeInfo.store_email}
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        {/* ======================= Footer bottom ================== */}
        <div className="border-t-1 border-mau-nau-vien flex justify-center">
          <div className="container p-2 flex justify-between items-center gap-8">
            <p className="text-sm md:text-base">
              Đang đói bụng? hãy đến khu vui chơi giải trí với giá chỉ 50k/trẻ
              em
            </p>
            {/* Social */}
            <ul className="flex items-center gap-x-4">
              <li className="relative w-9 h-9 z-1">
                <Link
                  to={"/"}
                  className="group block w-full h-full p-2.5 bg-transparent rounded-full duration-200 hover:bg-mau-sua-bo"
                >
                  <Icon.Facebook className="text-white w-full h-full duration-200 group-hover:text-primary" />
                </Link>
                <Icon.SocialBg className="absolute -inset-1 m-auto text-primary -z-1" />
              </li>
              <li className="relative w-9 h-9 z-1">
                <Link
                  to={"/"}
                  className="group block w-full h-full p-2 bg-transparent rounded-full duration-200 hover:bg-mau-sua-bo"
                >
                  <Icon.Instagram className="text-white w-full h-full duration-200 group-hover:text-primary" />
                </Link>
                <Icon.SocialBg className="absolute -inset-1 m-auto text-primary -z-1" />
              </li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;

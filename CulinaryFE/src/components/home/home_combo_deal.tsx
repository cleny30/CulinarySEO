import React from "react";
import { motion } from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { storeInfo } from "@/storeInfo";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { RatingStar } from "../ui/ratingStart";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";

type ComboKey =
  | "section_combo_item1_img"
  | "section_combo_item2_img"
  | "section_combo_item1_name"
  | "section_combo_item2_name"
  | "section_combo_item1_rating"
  | "section_combo_item2_rating"
  | "section_combo_item1_href"
  | "section_combo_item2_href"
  | "section_combo_item1_price"
  | "section_combo_item2_price";

function ComboHeader() {
  return (
    <div className="category_header flex flex-col items-center">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-primary font-semibold text-base lg:text-lg mb-2"
      >
        {storeInfo.section_combo_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase max-w-lg text-center"
      >
        {storeInfo.section_combo_title}
      </motion.h1>
    </div>
  );
}
function ComboList() {
  return (
    <div className="combo_container relative not-lg:px-4">
      <motion.div
        initial={{ opacity: 0, y: "50%" }}
        whileInView={{ opacity: 1, y: "0%" }}
        transition={{ duration: 0.7 }}
        viewport={{ once: true }}
        className="grid grid-col-1 md:grid-cols-2 gap-10"
      >
        {Array.from({ length: 2 }).map((_, index) => (
          <div className="flex flex-col items-stretch gap-5">
            <div className="group relative image_container overflow-hidden rounded-lg">
              <LazyLoadImage
                src={
                  storeInfo[
                    `section_combo_item${index + 1}_img` as ComboKey
                  ] as string
                }
                alt={
                  storeInfo[
                    `section_combo_item${index + 1}_name` as ComboKey
                  ] as string
                }
                className="object-cover"
              />
              <h2 className="absolute z-2 left-0 bottom-10 bg-gray-50 text-base flex items-start gap-x-3 p-3 pr-4 rounded-r-lg lg:-translate-x-full group-hover:translate-x-0 duration-700">
                Giá{" "}
                <span className="font-Lucky text-2xl lg:text-3xl">
                  {
                    storeInfo[
                      `section_combo_item${index + 1}_price` as ComboKey
                    ] as string
                  }
                  <span className="font-Mont">đ</span>
                </span>
              </h2>
              <div className="absolute inset-0 z-1 bg-gradient-to-t from-mau-do-color/90 via-mau-do-color/20 to-transparent lg:translate-y-full group-hover:translate-y-0 duration-500"></div>
            </div>
            <div className="combo_item_content flex items-center justify-around p-5 border rounded-lg flex-col lg:flex-row not-lg:gap-y-3">
              <div className="flex flex-col lg:items-start items-center space-y-4 lg:space-y-2">
                {/* star group */}
                <div className="flex items-center gap-x-4">
                  <RatingStar
                    allowHalf
                    readOnly
                    value={
                      storeInfo[
                        `section_combo_item${index + 1}_rating` as ComboKey
                      ] as number
                    }
                    size={16}
                    gapClass="gap-0.5"
                  />
                  <span
                    className="font-Lucky text-lg"
                    aria-label={`combo${index + 1}-rating`}
                  >
                    {
                      storeInfo[
                        `section_combo_item${index + 1}_rating` as ComboKey
                      ] as number
                    }
                  </span>
                </div>
                <h1 className="font-Lucky uppercase text-2xl lg:text-3xl">
                  {
                    storeInfo[
                      `section_combo_item${index + 1}_name` as ComboKey
                    ] as string
                  }
                </h1>
              </div>
              <Button type="button" variant={"outline"} size={"xl"} className="not-lg:bg-primary not-lg:text-white font-Lucky tracking-wider text-[.938rem] pt-1">
                <Link
                  to={
                    storeInfo[
                      `section_combo_item${index + 1}_href` as ComboKey
                    ] as string
                  }
                >
                  Xem ngay
                </Link>
              </Button>
            </div>
          </div>
        ))}
      </motion.div>
      <img
        src="/img/bg_item_leaf_2.webp"
        className="not-lg:hidden h-25 absolute z-10 top-0 left-0 -rotate-35 blur-xs -translate-x-1/2 -translate-y-1/2 brightness-110"
      />
      <motion.img
        initial={{ scale: 0, opacity: 0 }}
        whileInView={{ scale: 1, opacity: 1 }}
        transition={{ duration: 0.5, delay: 0.5 }}
        viewport={{ once: true }}
        src="/svg/edge_image_3line.svg"
        className="not-lg:hidden h-25 absolute z-10 top-0 right-0 translate-x-15 -translate-y-18"
      />
    </div>
  );
}
export default function SectionComboDeals() {
  return (
    <section className="section_combo py-4 pb-40">
      <div className="container mx-auto space-y-15">
        <ComboHeader />
        <ComboList />
      </div>
    </section>
  );
}

import { storeInfo } from "@/storeInfo";
import React from "react";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { motion } from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";

/**
 * 
 * @returns section_ctsu_show:
            section_ctsu_subtext:
            section_ctsu_title: 
            section_ctsu_subtitle:
            section_ctsu_img:
            section_ctsu_other_label:
            section_ctsu_other_sublabel:
            section_ctsu_btn_label:
            section_ctsu_btn_href:
 */

function IntroduceContent() {
  return (
    <div className="flex flex-col gap-y-3 lg:gap-y-4">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-primary font-semibold text-base lg:text-lg mb-2"
      >
        {storeInfo.section_ctsu_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase"
      >
        {storeInfo.section_ctsu_title}
      </motion.h1>
      <motion.p
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-subtitle text-[.938rem] lg:text-base"
      >
        {storeInfo.section_ctsu_subtitle}
      </motion.p>
    </div>
  );
}

function CtsuArea() {
  return (
    <div className="flex flex-col w-[65%] sm:w-[52%] md:w-[85%] gap-y-5 md:gap-y-10 relative">
      <div className="space-y-2 md:space-y-4 self-end">
        <h1 className="font-Lucky text-mau-do-color text-2xl md:text-4xl">
          {storeInfo.section_ctsu_other_label}
        </h1>
        <h3 className="font-Lucky text-xl">
          {storeInfo.section_ctsu_other_sublabel}
        </h3>
      </div>
      <Button
        variant={"outline"}
        size={"lg"}
        className="self-start bg-transparent font-Lucky md:py-6 text-base border-mau-do-color tracking-wide hover:bg-mau-do-color hover:text-white"
      >
        <Link to={storeInfo.section_ctsu_btn_href || "/register"}>
          {storeInfo.section_ctsu_btn_label}
        </Link>
      </Button>
      <img
        src="/svg/section_featuredProduct_arrow.svg"
        className={"absolute rotate-y-180 rotate-100 top-1/5 md:top-1/3 left-0"}
      />
    </div>
  );
}

export default function SectionCallToSignUp() {
  return (
    <section className="section_call_to_signup py-15 md:py-25 bg-mau-be relative overflow-hidden">
      <div className="container mx-auto not-md:px-4 not-lg:px-2">
        <div className="grid grid-cols-1 md:grid-cols-2 not-lg:gap-x-10  md:not-lg:h-180">
          <motion.div className="h-full">
            <LazyLoadImage
              src={storeInfo.section_ctsu_img}
              className="rounded-lg h-full object-cover"
            />
          </motion.div>
          <div className="grid place-items-center py-10">
            <div className="flex flex-col justify-center not-lg:gap-y-12 lg:justify-between md:max-w-[430px] h-full">
              <IntroduceContent />
              <CtsuArea />
            </div>
          </div>
        </div>
      </div>
      <motion.div
        className="absolute -right-8 top-1/2 not-lg:hidden opacity-50"
        animate={{
          transform: ["translateY(7%)", "translateY(-7%)", "translateY(7%)"],
        }}
        transition={{ repeat: Infinity, duration: 3, ease: "linear" }}
      >
        <img
          src="/svg/bg_item_bufalo_bread.svg"
          alt="bg_item_bread_footer"
          className="size-50"
        />
      </motion.div>
    </section>
  );
}

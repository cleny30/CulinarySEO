import { storeInfo, type storeInfoKey } from "@/storeInfo";
import { motion } from "framer-motion";
import { Icon, type IconName } from "@/utils/assets/icon";
import { textSkewIn } from "@/utils/constants/effect";


function IntroduceContent() {
  return (
    <div className="flex flex-col gap-y-4 lg:gap-y-6 w-full not-md:items-center">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-primary font-semibold text-base lg:text-lg"
      >
        {storeInfo.section_openingHour_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase"
      >
        {storeInfo.section_openingHour_title}
      </motion.h1>
      <motion.p
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-subtitle text-[.938rem] lg:text-base"
      >
        {storeInfo.section_openingHour_subtitle}
      </motion.p>
    </div>
  );
}

function TwoBanner() {
  return (
    <div className="relative flex-1">
      <motion.img
        loading="lazy"
        initial={{ x: "50%", opacity: 0 }}
        whileInView={{ x: 0, opacity: 1 }}
        viewport={{ once: true }}
        transition={{ duration: 0.5 }}
        src={storeInfo.section_openingHour_img}
        alt="section_introduce_img_1"
        className="rounded-lg w-full  object-cover not-md:aspect-square"
      />
    </div>
  );
}

function OpeningHourList() {
  const h1Style = "font-Lucky text-xl uppercase";
  const h5Style = "font-Mont text-base uppercase";
  return (
    <motion.div
      initial={{ y: "50%", opacity: 0 }}
      whileInView={{ y: 0, opacity: 1 }}
      viewport={{ once: true }}
      transition={{ duration: 0.5 }}
      className="grid grid-cols-1 gap-4 w-full"
    >
      {Array.from({ length: 3 }).map((_, index) => (
        <div className="flex items-center gap-4 justify-between" key={index}>
          <h1 className={h1Style}>
            {
              storeInfo[
                `section_openingHour_item_title${index + 1}` as storeInfoKey
              ]
            }
          </h1>
          <h5 className={h5Style}>
            {" "}
            {
              storeInfo[
                `section_openingHour_item_content${index + 1}` as storeInfoKey
              ]
            }
          </h5>
        </div>
      ))}
    </motion.div>
  );
}

function LeftLayout() {
  const IconName = storeInfo.section_openingHour_icon_list as IconName;
  const SelectedIcon = Icon[IconName];
  return (
    <div className="shrink-0 basis-[405px] flex flex-col items-center md:items-start gap-y-10 md:gap-y-20 justify-center py-4">
      <IntroduceContent />
      <div className="flex flex-col gap-y-4 md:gap-y-8 items-center md:items-start w-full max-w-[405px]">
        <motion.div
          initial={{
            x: 5,
            y: 5,
            transform: "rotate(-10deg)",
            opacity: 0,
          }}
          viewport={{ once: true }}
          whileInView={{
            transform: "rotate(0deg)",
            x: 0,
            y: 0,
            opacity: 1,
          }}
          transition={{ duration: 0.5 }}
          className="flex not-md:flex-col items-center gap-4"
        >
          <div className="size-12 grid place-items-center relative rounded-full z-1">
            <SelectedIcon className="text-white"/>
            <Icon.SocialBg className="absolute inset-0 text-primary -z-1 size-full" />
          </div>
          <p className="font-Lucky text-3xl">
            {storeInfo.section_openingHour_list_title}
          </p>
        </motion.div>
        <OpeningHourList />
      </div>
    </div>
  );
}

export default function SectionOpeningHour() {
  return (
    <section className="section_introduce py-25 not-md:pb-4 px-4 relative">
      <div className="container mx-auto">
        <div className="flex items-stretch justify-start gap-8 lg:gap-[10.8%] not-md:flex-col-reverse relative">
          <LeftLayout />
          <TwoBanner />
        </div>
      </div>
    </section>
  );
}

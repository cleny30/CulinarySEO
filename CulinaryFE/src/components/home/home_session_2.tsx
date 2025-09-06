import { storeInfo } from "@/storeInfo";
import { motion } from "framer-motion";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { Skeleton } from "../ui/skeleton";
import { Icon } from "@/utils/assets/icon";
import { textSkewIn } from "@/utils/constants/effect";

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
        {storeInfo.section_introduce_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase"
      >
        {storeInfo.section_introduce_title}
      </motion.h1>
      <motion.p
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-subtitle text-[.938rem] lg:text-base"
      >
        {storeInfo.section_introduce_subtitle}
      </motion.p>
    </div>
  );
}

function TwoBanner() {
  return (
    <div className="relative flex-1 md:pr-7.5 md:pb-21.5">
      <motion.img
        initial={{ x: "-50%", opacity: 0 }}
        whileInView={{ x: 0, opacity: 1 }}
        viewport={{ once: true }}
        transition={{ duration: 0.5 }}
        src={storeInfo.section_introduce_img_1}
        alt="section_introduce_img_1"
        className="rounded-lg w-full lg:w-[422px] md:h-[522px] object-cover not-md:aspect-square"
      />
      <motion.div
        initial={{ x: "25%", y: "25%", opacity: 0 }}
        whileInView={{ x: 0, y: 0, opacity: 1 }}
        viewport={{ once: true }}
        transition={{ duration: 0.5 }}
        className="pt-5 pl-5 bg-background absolute right-0 bottom-0 rounded-lg overflow-hidden not-md:hidden"
      >
        <img
          src={storeInfo.section_introduce_img_2}
          alt="section_introduce_img_2"
          className="rounded-lg w-[330px] h-[411px]"
        />
      </motion.div>
    </div>
  );
}

function AvatarGroup() {
  return (
    <div className="avatarGroup flex flex-row">
      {Array.from({ length: 3 }).map((_, index) => (
        <Avatar className="size-13 rounded-full -mr-3">
          <AvatarImage src={`/img/person${index + 1}.webp`} />
          <AvatarFallback>
            <Skeleton className="rounded-full size-full" />
          </AvatarFallback>
        </Avatar>
      ))}
      <div className="size-13 grid place-items-center bg-primary rounded-full z-1 -ml-1">
        <Icon.Plus className="text-white" />
      </div>
    </div>
  );
}

function RightLayout() {
  const h1Style = "font-Lucky text-primary text-4xl lg:text-6xl uppercase";
  const h5Style = "font-Lucky text-base uppercase";
  return (
    <div className="shrink-0 basis-[430px] lg:basis-[477px] flex flex-col items-start gap-y-8 justify-center lg:justify-between py-4">
      <IntroduceContent />
      <div className="flex flex-col gap-y-8 items-start">
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
          className="flex items-center gap-7"
        >
          <AvatarGroup />
          <p className="font-Lucky text-lg">
            Tin tưởng bởi 1k+ <br /> khách hàng
          </p>
        </motion.div>
        <div className="grid grid-cols-2 gap-4">
          <div className="flex flex-col items-start gap-2">
            <h1 className={h1Style}>40+</h1>
            <h5 className={h5Style}>Đơn mỗi ngày</h5>
          </div>
          <div className="flex flex-col items-start gap-2">
            <h1 className={h1Style}>69+</h1>
            <h5 className={h5Style}>Sản phẩm có sẵn</h5>
          </div>
        </div>
      </div>
    </div>
  );
}

export default function SectionIntroduce() {
  return (
    <section className="section_introduce py-20 px-4">
      <div className="container mx-auto">
        <div className="flex items-stretch justify-start gap-6 lg:gap-[10.8%] not-md:flex-col">
          <TwoBanner />
          <RightLayout />
        </div>
      </div>
    </section>
  );
}

import { storeInfo } from "@/storeInfo";
import React, { useRef, useState, type RefObject } from "react";
import { motion } from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { Button } from "../ui/button";
import { Icon } from "@/utils/assets/icon";
import styles from "@/assets/css/home.module.css";
import { cn } from "@/lib/utils";

function IntroduceContent({
  isPlaying,
  setIsPlaying,
  videoRef,
}: {
  isPlaying: boolean;
  setIsPlaying: React.Dispatch<React.SetStateAction<boolean>>;
  videoRef: RefObject<HTMLVideoElement | null>;
}) {
  const togglePlay = () => {
    if (!videoRef.current) return;
    if (isPlaying) {
      videoRef.current.pause();
    } else {
      videoRef.current.play();
    }
    setIsPlaying(!isPlaying);
  };
  return (
    <div className="flex flex-col gap-y-3 lg:gap-y-7 md:max-w-[323px] lg:max-w-[500px]">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-white font-semibold text-base lg:text-lg capitalize"
      >
        {storeInfo.section_videoRating_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-14 uppercase text-white"
      >
        {storeInfo.section_videoRating_title}
      </motion.h1>
      <motion.div
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="items-center flex gap-x-5"
      >
        <Button
          onClick={() => togglePlay()}
          size={"icon"}
          className="p-8 rounded-full"
        >
          {isPlaying ? (
            <Icon.Pause className="size-7 relative z-10" />
          ) : (
            <Icon.Play className="size-7 relative z-10" />
          )}
        </Button>
        <span className="font-Lucky text-3xl text-white tracking-wider">
          XEM VIDEO
        </span>
      </motion.div>
    </div>
  );
}
function RatingCard() {
  return (
    <div className="bg-black/35  px-9.5 pt-10 pb-3.75 border-1 border-mau-nau-vien/50 rounded-lg backdrop-blur-3xl">
      <div className="flex flex-col items-start gap-y-6 max-w-[410px]">
        <img
          src="/img/3People.webp"
          alt="group of person avatar"
          className=" h-15"
        />
        <p className="text-white pb-6 border-b-1 border-mau-nau-vien/25 text-lg">
          {storeInfo.section_videoRating_ratingDesc}
        </p>
        <h1 className="font-Lucky text-white flex items-center text-xl">
          <span className="mr-6 text-7xl">
            {storeInfo.section_videoRating_rating}
          </span>
          ( đánh giá )
        </h1>
      </div>
    </div>
  );
}
export default function HomeVideoRatingSection() {
  const videoRef = useRef<HTMLVideoElement>(null);
  const [isPlaying, setIsPlaying] = useState(true);

  return (
    <section className="section_videoRating">
      <div className="grid grid-cols-1 grid-rows-1 h-fit relative">
        <div className={cn("", styles.gridCol1, styles.gridRow1)}>
          <div className="w-full h-full relative">
            <video
              ref={videoRef}
              autoPlay
              muted
              loop
              playsInline
              className="absolute h-full w-full object-cover"
              poster={storeInfo.section_videoRating_video_poster}
            >
              <source
                src={storeInfo.section_videoRating_video_src}
                type="video/webm"
              />
              <source src={storeInfo.section_videoRating_video_srcAlter} />
            </video>
          </div>
        </div>
        <div className={cn("bg-black/50 z-1", styles.gridCol1, styles.gridRow1)}>
          <div className="container mx-auto h-fit not-lg:px-4">
            <div className="flex gap-x-5 justify-between items-start h-fit py-15 lg:py-60 not-md:flex-col not-md:gap-y-10">
              <IntroduceContent
                setIsPlaying={setIsPlaying}
                isPlaying={isPlaying}
                videoRef={videoRef}
              />
              <RatingCard />
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

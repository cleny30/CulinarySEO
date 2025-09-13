import React from "react";
import { motion } from "framer-motion";
import { storeInfo, type storeInfoKey } from "@/storeInfo";
import styles from "@/assets/css/home.module.css";

export default function SectionHomeService() {
  return (
    <section className="section_service py-25 not-md:px-4">
      <div className="container mx-auto">
        <div className="grid grid-cols-1 md:grid-cols-3 place-items-stretch">
          {Array.from({ length: 3 }).map((_, index) => (
            <motion.div
              className={styles.serviceItem}
              initial={{ y: "50%", opacity: 0 }}
              whileInView={{ y: "0%", opacity: 1 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5, delay: 0.3 * index }}
            >
              <div
                className="flex flex-col items-center md:max-w-[216px] lg:max-w-[300px] gap-y-4"
                key={index}
              >
                <img
                  className="size-15 mb-2 md:mb-4"
                  loading="lazy"
                  src={
                    storeInfo[
                      `section_service_item${index + 1}_img` as storeInfoKey
                    ] as string
                  }
                  alt={
                    storeInfo[
                      `section_service_item${index + 1}_title` as storeInfoKey
                    ] + " image"
                  }
                />
                <h1 className="font-Lucky uppercase text-xl lg:text-2xl text-center">
                  {
                    storeInfo[
                      `section_service_item${index + 1}_title` as storeInfoKey
                    ]
                  }
                </h1>
                <p className="text-[.938rem] lg:text-base text-center">
                  {
                    storeInfo[
                      `section_service_item${
                        index + 1
                      }_subtitle` as storeInfoKey
                    ]
                  }
                </p>
              </div>
            </motion.div>
          ))}
        </div>
      </div>
    </section>
  );
}

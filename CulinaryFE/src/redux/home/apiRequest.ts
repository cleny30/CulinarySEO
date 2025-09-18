import { getCateList, getFeatureProduct } from "@/services/homeService";
import type { AppDispatch } from "../store";
import { loadedCate, loadedHomeCate, loadedProduct } from "./homeSlice";
import { menuNav } from "@/utils/config/navMenu";
import type { Category } from "@/types/category";
import type { Product } from "@/types/product";

export const fetchCateMenu = async (dispatch: AppDispatch) => {
  const response = (await getCateList()) as Category[];

  const newCategories = response.map((item) => {
    return {
      label: item.categoryName,
      href: `/collections/${item.categoryId}`,
      image: item.categoryImage,
    };
  });

  const updatedMenuNav = menuNav.map((item) => {
    if (item.slug === "thuc-don") {
      return {
        ...item,
        children: newCategories,
      };
    }
    return item;
  });
  dispatch(loadedCate(updatedMenuNav));
  dispatch(loadedHomeCate(response));
};

export const fetch4FeaturedProduct = async (dispatch: AppDispatch) => {
  const response = (await getFeatureProduct()) as {
    items: Product[];
    page: number;
    pageSize: number;
    totalItems: number;
  };
  console.log(response)
  const top4Product = response!.items.slice(0, 4);

  dispatch(loadedProduct(top4Product));
};

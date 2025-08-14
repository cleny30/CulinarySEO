import { getCategories } from "@/services/filterService";
import type { AppDispatch } from "../store";
import { setCategories } from "./productfilterSlice";
import type { Category } from "@/types/filter";

export const getFilter = async (dispatch: AppDispatch) => {
    const response = await getCategories();
    if (response.error) {
        return { error: response.error };
    }
    dispatch(setCategories(response.data as Category[]));
    return;
};
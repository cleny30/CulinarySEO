import { getCategories } from "@/services/filterService";
import type { AppDispatch } from "../store";
import { setCategories, setFetching } from "./productfilterSlice";
import type { Category } from "@/types/filter";



export const getFilter = async (dispatch: AppDispatch) => {
    dispatch(setFetching(true));

    const response = await getCategories();
    if (response.error) {
        dispatch(setFetching(false));
        return { error: response.error };
    }
    dispatch(setCategories(response.data as Category[]));
    dispatch(setFetching(false));
    return;
};
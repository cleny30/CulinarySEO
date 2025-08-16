import { Checkbox } from "@/components/ui/checkbox"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
} from "@/components/ui/form";
import { useDispatch, useSelector } from 'react-redux'
import type { RootState } from '@/redux/store'
import { useEffect } from 'react'
import { getFilter } from "@/redux/product/apiRequest"
import { filterSchema, type FilterFormValues } from "@/schemas/filter";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { setFetching, setPrice, setAvailability } from "@/redux/product/productfilterSlice";
import { Input } from "../ui/input";
import { Slider } from "../ui/slider";
import FilterCard from "./FilterCard";
import { Skeleton } from "../ui/skeleton";

export default function FIlterSider() {
    const dispatch = useDispatch()
    const filterprops = useSelector((state: RootState) => state.productfilter)
    const getcategory = async () => (
        await getFilter(dispatch)
    )

    const form = useForm<FilterFormValues>({
        resolver: zodResolver(filterSchema),
        defaultValues: {
            categories: [],
            price: { from: 0, to: 1000 },
            availability: false,
            sortBy: null,
        },
    });

    const filterwatchValues = form.watch()

    // useEffect(() => {
    //     console.log(watchValues, 'watchValues')
    // }, [watchValues])

    useEffect(() => {
        getcategory()
    }, [])
    return (
        <section className='w-3/12 px-[15px]'>
            <div className='w-full pb-[30px] border-b-[1px] mb-[30px]'>
                <h6>Filters</h6>
                <FilterCard name={filterwatchValues} categories={filterprops.productfilter.categories} />
            </div>
            <Form {...form}>
                <form className='flex flex-col gap-[30px]'>
                    <div className='w-full pb-[30px] border-b-[1px]'>
                        <div className="w-full flex items-center justify-between">
                            <h6>Categories</h6>
                            <button className="text-sm text-gray-500 hover:text-gray-700 " onClick={(e) => {
                                e.preventDefault();
                                form.resetField("categories");
                            }}>Reset</button>
                        </div>
                        <div className='mt-[30px] w-full'>
                            {filterprops.fetching ?
                                (
                                    <div className="w-full space-y-4">
                                        <Skeleton className="h-6 w-full " />
                                        <Skeleton className="h-6 w-full " />
                                        <Skeleton className="h-6 w-full " />
                                    </div>
                                ) :
                                (
                                    filterprops.productfilter.categories.map((cat) => (
                                        <FormField
                                            key={cat.categoryId}
                                            control={form.control}
                                            name="categories"
                                            render={({ field }) => {
                                                const value = field.value || [];
                                                const isChecked = value.includes(cat.categoryId);

                                                return (
                                                    <div className="flex items-center justify-between">
                                                        <div className="flex items-center space-x-3">
                                                            <Checkbox
                                                                checked={isChecked}
                                                                onCheckedChange={(checked) => {
                                                                    if (checked) {
                                                                        field.onChange([...value, cat.categoryId]);
                                                                    } else {
                                                                        field.onChange(value.filter((id) => id !== cat.categoryId));
                                                                    }
                                                                }}
                                                                className="data-[state=checked]:bg-orange-500 data-[state=checked]:border-orange-500"
                                                            />
                                                            <span className="text-sm">{cat.categoryName}</span>
                                                        </div>
                                                        <span className="text-sm text-gray-500">{cat.productCount}</span>
                                                    </div>
                                                );
                                            }}
                                        />
                                    ))
                                )
                            }
                        </div>
                    </div>
                    <div className='w-full pb-[30px] border-b-[1px] gap-[30px]'>
                        <h6>Price</h6>
                        <div className="flex items-center justify-between">
                            <span className="text-sm text-gray-600 mb-3">The highest price is ???</span>
                            <button className="text-sm text-gray-500 hover:text-gray-700 mb-4" onClick={(e) => {
                                e.preventDefault();
                                form.resetField("price");
                            }}>Reset</button>
                        </div>
                        <FormField
                            control={form.control}
                            name="price"
                            render={({ field }) => (
                                <FormItem className="w-full">
                                    {/* Slider */}
                                    <FormControl>
                                        <Slider
                                            value={[field.value.from, field.value.to]}
                                            onValueChange={([from, to]) => {
                                                field.onChange({ from, to });
                                                dispatch(setPrice({ from, to }));
                                            }}
                                            max={100}
                                            step={1}
                                            className="w-full"
                                        />
                                    </FormControl>
                                    <div className="flex items-center justify-center">
                                        {/* From Input */}
                                        <div className="space-y-1">
                                            <FormLabel className="text-[11px] opacity-70">From</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="number"
                                                    value={field.value.from}
                                                    onChange={(e) => {
                                                        field.onChange({
                                                            from: Number(e.target.value),
                                                            to: field.value.to,
                                                        });
                                                        setPrice({ from: Number(e.target.value), to: field.value.to })
                                                    }}
                                                    min={0}
                                                />
                                            </FormControl>
                                        </div>
                                        <div className="px-2">
                                            <span>-</span>
                                        </div>
                                        {/* To Input */}
                                        <div className="space-y-1">
                                            <FormLabel className="text-[11px] opacity-70">To</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="number"
                                                    value={field.value.to}
                                                    onChange={(e) => {
                                                        field.onChange({
                                                            from: field.value.from,
                                                            to: Number(e.target.value),
                                                        })
                                                        setPrice({ from: field.value.from, to: Number(e.target.value) })
                                                    }}
                                                    min={0}
                                                />
                                            </FormControl>
                                        </div>
                                    </div>
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className='w-full pb-[30px] border-b-[1px] gap-[30px]'>
                        <FormField
                            control={form.control}
                            name="availability"
                            render={({ field }) => (
                                <FormItem>
                                    <h6>Availability</h6>
                                    <div className="flex items-center ">
                                        <FormControl>
                                            <Checkbox
                                                checked={field.value}
                                                onCheckedChange={(checked) => {
                                                    field.onChange(!!checked)
                                                    dispatch(setAvailability(!!checked));
                                                }}
                                                className="data-[state=checked]:bg-orange-500 data-[state=checked]:border-orange-500"
                                            />
                                        </FormControl>
                                        <FormLabel className="text-sm font-normal ml-2">In Stock</FormLabel>
                                    </div>
                                </FormItem>
                            )}
                        />
                    </div>
                </form>
            </Form>
            <div></div>
        </section>
    )
}

import { Checkbox } from "@/components/ui/checkbox"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { categories } from '@/utils/constants/objects/object'
import { useDispatch, useSelector } from 'react-redux'
import type { RootState } from '@/redux/store'
import { useEffect } from 'react'
import { getFilter } from "@/redux/product/apiRequest"
import { filterSchema, type FilterFormValues } from "@/schemas/filter";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { setChangeCategoryCheck } from "@/redux/product/productfilterSlice";
import { Input } from "../ui/input";
import { Slider } from "../ui/slider";

export default function FIlterSider() {
    const dispatch = useDispatch()
    const filterprops = useSelector((state: RootState) => state.productfilter.productfilter)
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

    const watchValues = form.watch()

    useEffect(() => {
        console.log(watchValues, 'watchValues')
    }, [watchValues])

    useEffect(() => {
        // Fetch categories or any other initial data if needed
        // getcategory()
    }, [])
    return (
        <div className='w-3/12 px-[15px]'>
            <div>
                <h6>Filters</h6>
                
            </div>
            <Form {...form}>
                <form className='flex flex-col gap-[30px]'>
                    <div className='w-full pb-[30px] border-b-[1px]'>
                        <h6>Categories</h6>
                        <div className='mt-[30px] w-full'>
                            {categories.map(cat => (
                                <FormField
                                    key={cat.name}
                                    control={form.control}
                                    name="categories"
                                    render={({ field }) => {
                                        const checked = field.value?.includes(cat.name) ?? false;
                                        return (
                                            <FormItem className="flex items-center justify-between">
                                                <div className="flex items-center space-x-3">
                                                    <FormControl >
                                                        <Checkbox
                                                            checked={checked}
                                                            onCheckedChange={(isChecked) => {
                                                                const newValue = isChecked
                                                                    ? [...(field.value || []), cat.name]
                                                                    : (field.value || []).filter(id => id !== cat.name);
                                                                field.onChange(newValue);
                                                                // Sync with Redux if you want
                                                                // dispatch(setChangeCategoryCheck({ categoryID: cat.name, checked: !!isChecked }));
                                                            }}
                                                            className="data-[state=checked]:bg-orange-500 data-[state=checked]:border-orange-500"
                                                        />
                                                    </FormControl>
                                                    <FormLabel className="text-sm font-normal">{cat.name}</FormLabel>
                                                </div>
                                                <FormLabel className="text-sm text-gray-500">({cat.count})</FormLabel>
                                            </FormItem>
                                        );
                                    }}
                                />
                            ))}
                        </div>
                    </div>
                    <div className='w-full pb-[30px] border-b-[1px] gap-[30px]'>
                        <h6>Price</h6>
                        <div className="flex items-center justify-between">
                            <span className="text-sm text-gray-600 mb-3">The highest price is ???</span>
                            <button className="text-sm text-gray-500 hover:text-gray-700 mb-4" onClick={() => {
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
                                            onValueChange={([from, to]) => field.onChange({ from, to })}
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
                                                    onChange={(e) =>
                                                        field.onChange({
                                                            from: Number(e.target.value),
                                                            to: field.value.to,
                                                        })
                                                    }
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
                                                    onChange={(e) =>
                                                        field.onChange({
                                                            from: field.value.from,
                                                            to: Number(e.target.value),
                                                        })
                                                    }
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
                                                onCheckedChange={(checked) => field.onChange(!!checked)}
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
        </div>
    )
}

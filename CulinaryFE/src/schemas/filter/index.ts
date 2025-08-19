import { z } from "zod";

export const filterSchema = z.object({
  categories: z.array(z.number()), // array of category IDs
  price: z.object({
    from: z.number().min(0),
    to: z.number().min(0)
  }).refine(data => data.to >= data.from, {
    message: "Max price must be greater than min price"
  }),
  availability: z.boolean().nullable(), // nullable boolean for availability
  sortBy: z.string().nullable() // or use z.nativeEnum(SortBy) if enum
});

export type FilterFormValues = z.infer<typeof filterSchema>;
import { store_email, store_phone_number } from "@/utils/config/storeInfo";
import * as z from "zod";

export const LoginSchema = z.object({
  email: z
    .string()
    .nonempty({ message: "Email shouldn't be empty" })
    .email({ message: "Enter a valid email - example@gmail.com" }),
  password: z
    .string()
    .min(6, { message: "Password must be from 6 to 18 characters!" })
    .max(18, { message: "Password must be from 6 to 18 characters!" })
    .regex(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{6,18}$/, {
      message: "Password must contain an Uppercase, lowercase, number!",
    }),
});

export const RegisterSchema = z.object({
  fullName: z.string().nonempty({ message: "Username shouldn't be empty" }),
  phone: z
    .string()
    .nonempty({ message: "Phone shouldn't be empty" })
    .regex(/^[0-9]{10,11}$/, {
      message: `Phone should be like ${store_phone_number}`,
    }),
  email: z
    .string()
    .nonempty({ message: "Email shouldn't be empty" })
    .email({ message: `Enter a valid email - ${store_email}` }),
  password: z
    .string()
    .min(6, { message: "Password must be from 6 to 18 characters!" })
    .max(18, { message: "Password must be from 6 to 18 characters!" })
    .regex(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{6,18}$/, {
      message: "Password must contain an Uppercase, lowercase, number!",
    }),
  repassword: z
    .string()
    .min(6, { message: "Password must be from 6 to 18 characters!" })
    .max(18, { message: "Password must be from 6 to 18 characters!" })
    .regex(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{6,18}$/, {
      message: "Password must contain an Uppercase, lowercase, number!",
    }),
  username: z.string().optional(),
})
.refine((data) => data.password === data.repassword, {
  message: "Passwords doesn't match",
  path: ["repassword"],
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;
export type RegisterSchemaType = z.infer<typeof RegisterSchema>;

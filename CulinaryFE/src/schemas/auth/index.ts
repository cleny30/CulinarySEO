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

export type LoginSchemaType = z.infer<typeof LoginSchema>;

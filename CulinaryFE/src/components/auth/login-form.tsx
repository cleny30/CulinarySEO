import CardWrapper from "./card-wrapper";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { LoginSchema, type LoginSchemaType } from "@/schemas/auth";
import { Input } from "../ui/input";
import { Icon } from "@/utils/assets/icon";
import { Button } from "../ui/button";
import { login } from "@/redux/auth/apiRequest";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import toast from "@/utils/toast";
import { useTransition } from "react";
import styles from "@/assets/css/auth.module.css";

export default function LoginForm() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [isPending, startTransition] = useTransition();

  const form = useForm<LoginSchemaType>({
    resolver: zodResolver(LoginSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const onSubmit = async (values: LoginSchemaType) => {
    startTransition(
      async () =>
        await login(values, dispatch, navigate).then((result) => {
          if (result?.error) toast.error(result.error as string);
          if (result?.success) toast.success(result.success as string);
        })
    );
  };

  return (
    <CardWrapper
      showSocial
      backButtonLabel="Don't have an account"
      backButtonHref="/register"
    >
      <Form {...form}>
        <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
          <div className="flex flex-col mb-2">
            <FormField
              disabled={isPending}
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem className={`${styles.formItem} mb-4`}>
                  <FormLabel className="text-gray-500">
                    <Icon.Email className="w-5" />
                  </FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="email"
                      placeholder="example@gmail.com"
                      className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              disabled={isPending}
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem className={`${styles.formItem} mb-1`}>
                  <FormLabel className="text-gray-500">
                    <Icon.Lock className="w-5" />
                  </FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="password"
                      placeholder="password from 6 to 18 character"
                      className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="flex justify-end mb-4">
              <Button variant={"link"} className="p-0 font-normal" asChild>
                <Link to={""}>Forgot password ?</Link>
              </Button>
            </div>
            <Button
              className="cursor-pointer p-5"
              type="submit"
              variant={"default"}
              disabled={isPending}
            >
              Login
            </Button>
          </div>
        </form>
      </Form>
    </CardWrapper>
  );
}

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
import { useNavigate } from "react-router-dom";
import toast from "@/utils/toast";
import { useTransition } from "react";

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
    <CardWrapper>
      <Form {...form}>
        <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
          <div className="flex flex-col gap-y-4">
            <FormField
              disabled={isPending}
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel> {Icon.Email} Email</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="email"
                      placeholder="example@gmail.com"
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
                <FormItem>
                  <FormLabel> {Icon.Lock} Password</FormLabel>
                  <FormControl>
                    <Input
                      {...field}
                      type="password"
                      placeholder="password from 6 to 18 character"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            {/* TODO: add form error here */}
            <Button className="cursor-pointer" type="submit" variant={"outline"} disabled={isPending}>
              Login
            </Button>
          </div>
        </form>
      </Form>
    </CardWrapper>
  );
}

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
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import type { RootState } from "@/redux/store";
import toast from "@/utils/toast";

export default function LoginForm() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const pending = useSelector(
    (state: RootState) => state.auth.login.isFetching
  );
  const isError = useSelector((state: RootState) => state.auth.login.error);

  const form = useForm<LoginSchemaType>({
    resolver: zodResolver(LoginSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const onSubmit = async (values: LoginSchemaType) => {
    await login(values, dispatch, navigate).then((result) => {
      if (isError) toast.error(result?.error as string);
      else toast.success(result?.success as string);
    });
  };

  return (
    <CardWrapper>
      <Form {...form}>
        <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
          <div>
            <FormField
              disabled={pending}
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel> {Icon.Email} </FormLabel>
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
              disabled={pending}
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem>
                  <FormLabel> {Icon.Lock} </FormLabel>
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
            <Button type="submit" variant={"outline"} disabled={pending}>
              Login
            </Button>
          </div>
        </form>
      </Form>
    </CardWrapper>
  );
}

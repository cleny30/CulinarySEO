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
import { RegisterSchema, type RegisterSchemaType } from "@/schemas/auth";
import { Input } from "../ui/input";
import { Icon } from "@/utils/assets/icon";
import { Button } from "../ui/button";
import toast from "@/utils/toast";
import { useState, useTransition } from "react";
import styles from "@/assets/css/auth.module.css";
import { storeInfo } from "@/storeInfo";
import { sentOtp } from "@/redux/auth/apiRequest";
import { useDependencyInjection } from "@/utils/hooks/useDependencyInjection";

export default function SignUpForm() {
  const { dispatch, navigate } = useDependencyInjection();

  const [isVisible, setVisible] = useState(false);
  const [isPending, startTransition] = useTransition();

  const handleTogglePass = () => {
    setVisible(!isVisible);
  };

  const form = useForm<RegisterSchemaType>({
    resolver: zodResolver(RegisterSchema),
  });

  const onSubmit = async (values: RegisterSchemaType) => {
    startTransition(
      async () =>
        await sentOtp(values, dispatch, navigate).then((result) => {
          if (result?.error) toast.error(result.error as string);
          if (result?.success) toast.success(result.success as string);
        })
    );
  };

  return (
    <CardWrapper
      showSocial
      backButtonLabel="Already have an account"
      backButtonHref="/login"
    >
      <Form {...form}>
        <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
          <div className="flex flex-col mb-2">
            <FormField
              disabled={isPending}
              control={form.control}
              name="fullName"
              render={({ field }) => (
                <div className="flex flex-col gap-y-1 mb-4">
                  <FormItem className={`${styles.formItem}`}>
                    <FormLabel className="text-gray-500">
                      <Icon.User className="w-5" />
                    </FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type="text"
                        placeholder="Nguyen Van A"
                        className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                      />
                    </FormControl>
                  </FormItem>
                  <FormMessage className="pl-1" />
                </div>
              )}
            />
            <FormField
              disabled={isPending}
              control={form.control}
              name="phone"
              render={({ field }) => (
                <div className="flex flex-col gap-y-1 mb-4">
                  <FormItem className={`${styles.formItem}`}>
                    <FormLabel className="text-gray-500">
                      <Icon.Smartphone className="w-5" />
                    </FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type="text"
                        placeholder={`ex: ${storeInfo.store_phone_number}`}
                        className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                      />
                    </FormControl>
                  </FormItem>
                  <FormMessage className="pl-1" />
                </div>
              )}
            />
            <FormField
              disabled={isPending}
              control={form.control}
              name="email"
              render={({ field }) => (
                <div className="flex flex-col gap-y-1 mb-4">
                  <FormItem className={`${styles.formItem}`}>
                    <FormLabel className="text-gray-500">
                      <Icon.Email className="w-5" />
                    </FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type="email"
                        placeholder={storeInfo.store_email}
                        className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                      />
                    </FormControl>
                  </FormItem>
                  <FormMessage className="pl-1" />
                </div>
              )}
            />
            <FormField
              disabled={isPending}
              control={form.control}
              name="password"
              render={({ field }) => (
                <div className="flex flex-col gap-y-1 mb-4">
                  <FormItem className={`${styles.formItem}`}>
                    <FormLabel className="text-gray-500">
                      <Icon.Lock className="w-5" />
                    </FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type={isVisible ? "text" : "password"}
                        placeholder="password from 6 to 18 character"
                        className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                      />
                    </FormControl>
                    <FormLabel className={`text-gray-500`}>
                      {isVisible ? (
                        <Icon.Eye className="w-5" onClick={handleTogglePass} />
                      ) : (
                        <Icon.EyeClosed
                          className="w-5"
                          onClick={handleTogglePass}
                        />
                      )}
                    </FormLabel>
                  </FormItem>
                  <FormMessage className="pl-1" />
                </div>
              )}
            />
            <FormField
              disabled={isPending}
              control={form.control}
              name="repassword"
              render={({ field }) => (
                <div className="flex flex-col gap-y-1 mb-1">
                  <FormItem className={`${styles.formItem}`}>
                    <FormLabel className="text-gray-500">
                      <Icon.CornerDownRight className="w-5" />
                    </FormLabel>
                    <FormControl>
                      <Input
                        {...field}
                        type={isVisible ? "text" : "password"}
                        placeholder="confirm your password"
                        className={`${styles.formInput} border-none shadow-none focus-visible:ring-0`}
                      />
                    </FormControl>
                  </FormItem>
                  <FormMessage className="pl-1" />
                </div>
              )}
            />
            <div className="flex justify-end mb-5">
              {/* TODO: add accept policy if needed */}
            </div>
            <Button
              className="cursor-pointer"
              type="submit"
              variant={"default"}
              disabled={isPending}
              size={"xl"}
            >
              Create account
            </Button>
          </div>
        </form>
      </Form>
    </CardWrapper>
  );
}

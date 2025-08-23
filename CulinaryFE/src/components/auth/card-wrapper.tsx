import React from "react";
import { Card, CardContent, CardFooter, CardHeader } from "../ui/card";
import Social from "./social";
import BackButton from "./back-button";

interface CardWrapperProps {
  children: React.ReactNode;
  showSocial?: boolean;
  backButtonHref?: string;
  backButtonLabel?: string;
}

export default function CardWrapper({
  children,
  showSocial,
  backButtonHref = "/",
  backButtonLabel = "",
}: CardWrapperProps) {
  return (
    <Card className="w-[600px] gap-y-4 shadow-none items-center">
      <CardHeader className="max-w-[500px] w-full">{showSocial && <Social />}</CardHeader>
      <CardContent className="max-w-[500px] w-full">{children}</CardContent>
      <CardFooter className="max-w-[500px] w-full">
        <BackButton href={backButtonHref} label={backButtonLabel} />
      </CardFooter>
    </Card>
  );
}

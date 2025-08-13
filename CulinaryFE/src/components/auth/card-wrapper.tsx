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
    <Card className="w-[400px] shadow-md gap-y-4">
      <CardHeader>{showSocial && <Social />}</CardHeader>
      <CardContent>{children}</CardContent>
      <CardFooter>
        <BackButton href={backButtonHref} label={backButtonLabel} />
      </CardFooter>
    </Card>
  );
}

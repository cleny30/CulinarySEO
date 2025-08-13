import React from "react";
import { Card, CardContent, CardFooter, CardHeader } from "../ui/card";
import Social from "./social";

interface CardWrapperProps {
  children: React.ReactNode;
  headerLabel?: string;
  showSocial?: boolean;
}

export default function CardWrapper({
  children,
  headerLabel,
  showSocial,
}: CardWrapperProps) {
  return (
    <Card className="w-[400px] shadow-md">
      <CardHeader>{headerLabel}</CardHeader>
      <CardContent>{children}</CardContent>
      <CardFooter>{showSocial && <Social />}</CardFooter>
    </Card>
  );
}

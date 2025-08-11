
interface HeaderCardProps {
  headerTitle: string;
  headerSubTitle?: string;
}
export default function HeaderCard({
  headerTitle,
  headerSubTitle,
}: HeaderCardProps) {
  return (
    <div className="text-center">
      <h1 className="mb-5">{headerTitle}</h1>
      <h3 className="mb-11">{headerSubTitle}</h3>
    </div>
  );
}

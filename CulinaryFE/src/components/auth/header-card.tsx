interface HeaderCardProps {
  headerTitle: React.ReactNode;
  headerSubTitle?: React.ReactNode;
}
export default function HeaderCard({
  headerTitle,
  headerSubTitle,
}: HeaderCardProps) {
  return (
    <div className="text-center flex flex-col items-center">
      <h1 className="mb-5 text-3xl font-Lucky">{headerTitle}</h1>
      {headerSubTitle && <h3 className="mb-8 text-base text-center text-gray-600 font-Mont">{headerSubTitle}</h3>}
    </div>
  );
}

import { Box, Pagination as MuiPagination } from "@mui/material";

interface Props {
  page: number;
  totalPages: number;
  onChange: (page: number) => void;
}

export default function Pagination({ page, totalPages, onChange }: Props) {
  if (totalPages <= 1) return null;

  return (
    <Box sx={{ display: "flex", justifyContent: "center", mt: 3 }}>
      <MuiPagination
        count={totalPages}
        page={page}
        onChange={(_, p) => onChange(p)}
        showFirstButton
        showLastButton
        color="primary"
      />
    </Box>
  );
}

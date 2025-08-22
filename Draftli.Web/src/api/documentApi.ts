import { apiBaseUrl } from "../consts/apiEndpoints";

  export const updateDocument = async (documentId: string, content: string) => {
    await fetch(`${apiBaseUrl}/api/documents/${documentId}/update`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(content),
    });
  };
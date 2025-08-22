import { useEffect, useState } from "react"
import * as signalR from "@microsoft/signalr"
import * as signalRMethods from "./consts/signalRMethods";
import { updateDocument } from "./api/documentApi";
import { apiBaseUrl } from "./consts/apiEndpoints";
import type { DocumentDtoType } from "./types/DocumentDto";

function App() {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [documentId] = useState("00000000-0000-0000-0000-000000000001");
  const [content, setContent] = useState("");
  const [version, setVersion] = useState(0);

  useEffect(() => {
    const conn = new signalR.HubConnectionBuilder()
      .withUrl(apiBaseUrl + "/hubs/document")
      .withAutomaticReconnect()
      .build();

    conn.start().then(() => {
      conn.invoke(signalRMethods.joinDocumentMethod, documentId);

      conn.on(signalRMethods.documentUpdatedMethod, (update: DocumentDtoType) => {
        if (update.documentId === documentId) {
          setContent(update.content);
          setVersion(update.version);
        }
      });
    });

    setConnection(() => conn);

    return () => {
      conn.stop();
    };
  }, [documentId]);
      
    return (
    <div style={{ padding: "2rem" }}>
      <h1>Draftli Test Editor</h1>
      <p>Document ID: {documentId}</p>
      <p>Version: {version}</p>
      <textarea
        value={content}
        onChange={(e) => setContent(e.target.value)}
        rows={10}
        cols={60}
      />
      <br />
      <button onClick={() => updateDocument(documentId, content)}>Save</button>
    </div>
  );
}

export default App

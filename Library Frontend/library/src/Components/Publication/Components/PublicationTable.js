import React from 'react';
import Table from './Table';
import TableRow from './TableRow';

export default function PublicationTable({ publicationList }) {
  return (
    <Table
      tableName="Publications based on your filters"
      columnNames={['Title', 'Authors', 'Type', 'Genre', 'Number Of Pages', 'Quantity']}
    >
      {publicationList.map((publication) => (
        <TableRow
          key={publication.id}
          rowData={[
            publication.title,
            publication.authors,
            publication.type,
            publication.genre,
            publication.numberOfPages,
            publication.quantity,
          ]}
        />
      ))}
    </Table>
  );
}
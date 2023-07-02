import React from 'react';

export default function MissingPage() {
  return (
    <div style={styles.missingPage}>
      <h1 style={styles.heading}>Oops! This page doesn't exist.</h1>
      <p style={styles.paragraph}>
        Sorry, but the page you are looking for could not be found.
      </p>
    </div>
  );
}

const styles = {
  missingPage: {
    backgroundColor: '#f8f8f8',
    padding: '40px',
    textAlign: 'center',
    fontFamily: 'Arial, sans-serif',
  },
  heading: {
    fontSize: '32px',
    color: '#333',
    marginBottom: '20px',
  },
  paragraph: {
    fontSize: '18px',
    color: '#666',
  },
};

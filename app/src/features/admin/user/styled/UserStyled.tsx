import styled from 'styled-components';

export const User = styled.div`
  display: flex;
  gap: 1em;
  flex-direction: column;

  > div:first-child {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
  }

  form {
    padding: 0.5em;
    border-radius: 0.25em;
    background-color: ${(props) => props.theme.css.formBackgroundColor};

    input[name='key'] {
      min-width: 38ch;
    }
  }
`;

import styled from 'styled-components';

export const Draft = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;

  h1 {
    color: ${(props) => props.theme.css.light};
    align-self: center;
  }

  img {
    width: 50em;
    border-radius: 0.25em;
  }
`;

export default Draft;

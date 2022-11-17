import styled from 'styled-components';

export const ApplyButton = styled.div`
  cursor: pointer;

  svg.off {
    color: ${(props) => props.theme.css.dangerColor};
  }

  svg.on {
    color: ${(props) => props.theme.css.primaryColor};
  }

  &:hover svg {
    filter: brightness(200%);
  }
`;
